using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartMaintApi.Models
{
    public class User : IdentityUser, IAuditTrail
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public AuditTrail Audit { get; set; } = new AuditTrail();

        // [Required]
        // public string? LastAction { get; set; }
        // [Required]
        // public string? UpdateUser { get; set; }
        // [Required]
        // public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}