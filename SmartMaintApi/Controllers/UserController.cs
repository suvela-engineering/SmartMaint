using System;
using System.IO.Compression;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        {
            return NotFound();
        }

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

        user.TimeStamp = DateTime.UtcNow; // Update modification time
        user.UpdateUser = HttpContext?.User?.Identity?.Name; // Set current user for audit trail
        // user.Audit.LastAction = HttpContext. // Here PUT, POST, DELETE, READ

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


    //     [HttpDelete("{id}")]
    //     public async Task<IActionResult> DeleteUser(string id)
    //     {
    //         var user = await _context.FindByIdAsync(id);
    //         if (user == null)
    //         {
    //             return NotFound();
    //         }

    //         user.TimeStamp = DateTime.UtcNow; // Update modification time
    //         user.UpdateUser = HttpContext?.User?.Identity?.Name; // Set current user for audit trail
    //                                                              // user.LastAction = HttpContext. // Here PUT, POST, DELETE, READ

    //         // Persist changes to the database
    //         _context.Users.Update(user);
    //         await _context.SaveChangesAsync();


    //         return NoContent();
    //     }
}