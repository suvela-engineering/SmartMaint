using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace SmartMaintApi.Models
{
    public class AuditTrail
    {
        [Required]
        public string? LastAction { get; set; }
        [Required]
        public string? UpdateUser { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        // public DateTime CreateDate { get; set; }
        // public string? CreateUser { get; set; } 
        // public DateTime? ModifiedDate { get; set; }
        // public string? ModifiedUser { get; set; }
        // public DateTime? Deleted { get; set; }
        // public string? DeleteUser { get; set; }
    }

    public interface IAuditTrail
    {
        AuditTrail Audit { get; set; }
    }
}