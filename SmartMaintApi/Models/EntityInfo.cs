using Microsoft.EntityFrameworkCore;

namespace SmartMaintApi.Models;

[Owned]
public class EntityInfo
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? Deleted { get; set; }
    public string? DeleteBy { get; set; }
}
