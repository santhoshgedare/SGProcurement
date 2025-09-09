using Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    [Index(nameof(UserId))]
    [Index(nameof(LoginAt))]
    [Index(nameof(LogoutAt))]
    [Index(nameof(IsActive))]
    public class UserSession : RootEntity
    {
        // Foreign key
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [MaxLength(45)] // IPv6 max length
        public string IpAddress { get; set; } = string.Empty;

        [MaxLength(500)]
        public string UserAgent { get; set; } = string.Empty;

        [MaxLength(100)]
        public string DeviceType { get; set; } = string.Empty;
        // e.g., Mobile, Desktop, Tablet

        [MaxLength(100)]
        public string Browser { get; set; } = string.Empty;
        // Chrome, Edge, Firefox etc.

        [MaxLength(100)]
        public string OperatingSystem { get; set; } = string.Empty;
        // Windows 11, Android 14, iOS 18

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;
        // optional – city/country via IP lookup

        public DateTime LoginAt { get; set; } = DateTime.UtcNow;
        public DateTime? LogoutAt { get; set; }

        public bool IsActive {get; set; }  
        public bool ExpiredAutomatically { get; set; } 
    }


}
