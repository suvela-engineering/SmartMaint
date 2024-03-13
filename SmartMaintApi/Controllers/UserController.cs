using System.Net;
using Microsoft.AspNetCore.Mvc;
using SmartMaintApi.Models;


[Route("API/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController(ApiDbContext context) => _context = context;
    private readonly ApiDbContext _context;

    // **Read User**
    [HttpGet("{UserId}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(int UserId)
    {
        var user = await _context.FindAsync<User>(UserId);

        if (user == null)
            return NotFound();

        // if (user.EntityInfo.Deleted.HasValue)
        //     return ResourceGone();

        return Ok(user);
    }

    // **Create User** (assuming password is handled separately)
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetUser", new { UserId = user.UserId }, user);
    }

    // **Update User** (excluding audit trail updates)
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateUser(string id, [FromBody] User userUpdate)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
    //     }

    //     var user = await _userManager.FindByIdAsync(id);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }

    //     // Update relevant user properties (exclude audit trail)
    //     user.UserName = userUpdate.UserName;
    //     // ...

    //     user.TimeStamp = DateTime.UtcNow; // Update modification time
    //     user.UpdateUser = HttpContext?.User?.Identity?.Name; // Set current user for audit trail
    //     // user.Audit.LastAction = HttpContext. // Here PUT, POST, DELETE, READ


    //         _context.Users.Update(user);
    //         await _context.SaveChangesAsync();

    //     return NoContent();
    // }
    //     // **Delete User** (soft delete with audit trail)


    [HttpDelete("{id}")]
    // [ProducesResponseType(statusCode: 204, type: typeof(void))]
    [ProducesResponseType(statusCode: 204, type: typeof(void))] // 204 - No Content: User deleted successfully
    [ProducesResponseType(statusCode: 404, type: typeof(void))] // 404 - Not Found
    [ProducesResponseType(statusCode: 410, type: typeof(void))] // 410 - Client Error (Resource Gone)
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.FindAsync<User>(id);

        if (user == null)
            return NotFound();

        if (user.EntityInfo.Deleted.HasValue)
            return StatusCode((int)HttpStatusCode.Gone);

        user.EntityInfo.Deleted = DateTime.UtcNow;

        // TO DO: Implement user who made the query/logged. Same to Interceptor
        user.EntityInfo.DeleteBy = HttpContext?.User?.Identity?.Name ?? "Unknown User";

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
}