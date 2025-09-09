using Core.Entities.Common;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class User : IdentityUser<Guid>, IRootEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public string FullName => string.Join(" ",
            new[] { FirstName, MiddleName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));
        public UserTypeEnum UserType { get; set; } = UserTypeEnum.Public;

        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;
        public Guid? DeletedBy { get; set; }

        public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
    }
}
