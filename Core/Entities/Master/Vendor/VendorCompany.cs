using Core.Entities.Common;
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
    [Index(nameof(Name))]
    [Index(nameof(GstNumber), IsUnique = true)]
    [Index(nameof(Email))]
    public class VendorCompany : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(15)] // GSTIN in India is 15 chars
        public string GstNumber { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)] // Phone numbers usually < 20 chars (with +country code)
        public string Contact { get; set; } = string.Empty;

        public ICollection<VendorUser> Users { get; set; } = new List<VendorUser>();
    }
}
