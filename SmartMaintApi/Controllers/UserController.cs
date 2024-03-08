using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMaintApi.Models;


public class UserController() : ControllerBase
{
    // private readonly ApiDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly DbContextOptions<ApiDbContext> _contextOptions;


    // Parameterized constructor for dependency injection
    public UserController( /*ApiDbContext context,*/ UserManager<User> userManager, DbContextOptions<ApiDbContext> contextOptions) : this()
    {
        // _context = context;
        _userManager = userManager;
        _contextOptions = contextOptions;
    }


    // **Read User**
    [HttpGet("api/users/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // **Create User** (assuming password is handled separately)
    [HttpPost("api/users")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }

        user.Audit.TimeStamp = DateTime.UtcNow; // Update modification time
        user.Audit.UpdateUser = HttpContext?.User?.Identity?.Name; // Set current user for audit trail
        // user.Audit.LastAction = HttpContext. // Here PUT, POST, DELETE, READ

        using (var context = new ApiDbContext(_contextOptions))
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        return CreatedAtRoute("GetUser", new { id = user.Id }, user);
    }

    // **Update User** (excluding audit trail updates)
    [HttpPut("api/users/{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User userUpdate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Update relevant user properties (exclude audit trail)
        user.UserName = userUpdate.UserName;
        // ...

        user.Audit.TimeStamp = DateTime.UtcNow; // Update modification time
        user.Audit.UpdateUser = HttpContext?.User?.Identity?.Name; // Set current user for audit trail
        // user.Audit.LastAction = HttpContext. // Here PUT, POST, DELETE, READ

        using (var context = new ApiDbContext(_contextOptions))
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }
    // **Delete User** (soft delete with audit trail)
    [HttpDelete("api/users/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Audit.TimeStamp = DateTime.UtcNow; // Update modification time
        user.Audit.UpdateUser = HttpContext?.User?.Identity?.Name; // Set current user for audit trail
        // user.Audit.LastAction = HttpContext. // Here PUT, POST, DELETE, READ

        // Persist changes to the database
        using (var context = new ApiDbContext(_contextOptions)) // Consider using the injected context
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }
}