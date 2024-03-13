using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SmartMaintApi.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public EntityInfo EntityInfo { get; set; } = new EntityInfo();

    }
}