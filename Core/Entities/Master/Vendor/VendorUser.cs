using Core.Entities.Common;
using Core.Entities.Identity;
using Core.Entities.Master.Buyer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Master.Vendor
{ 
    [Index(nameof(EmployeeCode))] 
    public class VendorUser : BaseEntity
    {
        [Required, MaxLength(50)]
        public string EmployeeCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Designation { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        public Guid VendorCompanyId { get; set; }
        public VendorCompany? VendorCompany { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
