using Core.Entities.Common;
using Core.Entities.Identity;
using Core.Entities.Master.Buyer;
using Core.Entities.Master.Vendor;
using Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Infrastructure.Data
{
    public class SGPContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SGPContext(DbContextOptions<SGPContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options) => _httpContextAccessor = httpContextAccessor;

        public Guid? CurrentUserId =>
            Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
                ? id
                : null;





        public DbSet<BuyerCompany> BuyerCompanies { get; set; }
        public DbSet<BuyerUser> BuyerUsers { get; set; }
        public DbSet<VendorCompany> VendorCompanies { get; set; }
        public DbSet<VendorUser> VendorUsers { get; set; }

        public DbSet<UserSession> UserSessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply all configurations in assembly
            builder.ApplyConfigurationsFromAssembly(typeof(SGPContext).Assembly);

            // Configure Identity table names
            ConfigureIdentityTables(builder);

            // Apply soft delete to all entities
            ConfigureSoftDelete(builder);
             
        }

        private static void ConfigureIdentityTables(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }

        private static void ConfigureSoftDelete(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(RootEntity).IsAssignableFrom(entityType.ClrType) ||
                typeof(IRootEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var entity = builder.Entity(entityType.ClrType);

                    entity.Property<bool>("IsDeleted");
                    entity.Property<DateTime?>("DeletedAt");
                    entity.Property<Guid?>("DeletedBy");

                    entity.HasIndex("IsDeleted");

                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var isDeleted = Expression.Call(
                        typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(bool)),
                        parameter,
                        Expression.Constant("IsDeleted")
                    );
                    var filter = Expression.Lambda(Expression.Equal(isDeleted, Expression.Constant(false)), parameter);
                    entity.HasQueryFilter(filter);
                }
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>().HaveColumnType("varchar(50)");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudit()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IRootEntity auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedAt = now;
                        auditable.CreatedBy = CurrentUserId;
                        auditable.UpdatedAt = now;
                        auditable.UpdatedBy = CurrentUserId;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedAt = now;
                        auditable.UpdatedBy = CurrentUserId;
                    }
                }

                if (entry.Entity is BaseEntity baseEntity)
                {
                    if (entry.State == EntityState.Added)
                        baseEntity.UpdateAudit(CurrentUserId);
                    else if (entry.State == EntityState.Modified)
                        baseEntity.UpdateAudit(CurrentUserId);
                }

                if (entry.Entity is RootEntity && entry.State == EntityState.Deleted)
                {
                    SoftDelete(entry, now);
                }
            }
        }
        private void SoftDelete(EntityEntry entry, DateTime now)
        {
            entry.State = EntityState.Modified;
            entry.CurrentValues["IsDeleted"] = true;
            entry.CurrentValues["DeletedAt"] = now;
            entry.CurrentValues["DeletedBy"] = CurrentUserId;

            foreach (var nav in entry.Navigations)
            {
                if (nav.CurrentValue is IEnumerable<RootEntity> collection)
                {
                    foreach (var child in collection)
                        SoftDelete(Entry(child), now);
                }
                else if (nav.CurrentValue is RootEntity child)
                {
                    SoftDelete(Entry(child), now);
                }
            }
        }

        // Restore deleted entities
        public void Restore<T>(T entity) where T : RootEntity
        {
            var entry = Entry(entity);
            entry.CurrentValues["IsDeleted"] = false;
            entry.CurrentValues["DeletedAt"] = null;
            entry.CurrentValues["DeletedBy"] = null;

            if (entity is BaseEntity baseEntity) baseEntity.UpdateAudit(CurrentUserId);
            else if (entity is IRootEntity auditable)
            {
                auditable.UpdatedAt = DateTime.UtcNow;
                auditable.UpdatedBy = CurrentUserId;
            }

            foreach (var nav in entry.Navigations)
            {
                if (nav.CurrentValue is IEnumerable<RootEntity> collection)
                    foreach (var child in collection) Restore(child);
                else if (nav.CurrentValue is RootEntity child)
                    Restore(child);
            }
        }

        public IQueryable<T> GetDeleted<T>() where T : RootEntity
        {
            return Set<T>().IgnoreQueryFilters().Where(e => EF.Property<bool>(e, "IsDeleted"));
        }
    }
}
