 
namespace Core.Entities.Common
{
    public abstract class RootEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
