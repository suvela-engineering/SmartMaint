using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace SmartMaintApi.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        [JsonIgnore] // Exclude from serialization
        public EntityInfo EntityInfo { get; set; } = new EntityInfo();

    }
}