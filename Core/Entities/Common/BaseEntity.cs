using System;

namespace Core.Entities.Common
{
    public abstract class BaseEntity : RootEntity
    {
        public Guid? CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Guid? UpdatedBy { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        public void UpdateAudit(Guid? userId)
        {
            if (CreatedDate == default)
            {
                CreatedBy = userId;
                CreatedDate = DateTime.UtcNow;
            }
            else
            {
                UpdatedBy = userId;
                UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}
