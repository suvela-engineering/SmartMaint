using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartMaintApi.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LastAction { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? TimeStamp { get; set; } = DateTime.UtcNow;
        public EntityInfo EntityInfo { get; set; } = new EntityInfo();

    }
}