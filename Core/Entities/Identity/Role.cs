using Core.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class Role : IdentityRole<Guid>, IRootEntity
    {
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }   
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid? UpdatedBy { get; set; }
    }
}
