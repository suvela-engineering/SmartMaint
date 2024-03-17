using System.Collections;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartMaintApi.Models;

namespace SmartMaintApi.Services;
public class UserService : IUserService
{
    public UserService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> GetUserAsync(int userId)
    {
        var user = await _context.FindAsync<User>(userId);

        if (user == null)
            return new NotFoundResult();

        else if (user.EntityInfo.Deleted.HasValue)
            return new NotFoundResult();

        return new OkObjectResult(user);
    }

    public async Task<IEnumerable> GetAllUsersAsync()
    {
        // Lazy loading
        var users = await _context.Users
            .Where(u => u.EntityInfo.Deleted.HasValue == false)
            .OrderBy(u => u.UserName)
            .ToListAsync();
        return users;
    }

    private readonly ApiDbContext _context;
    public async Task<IActionResult> UpdateUserAsync(int userId, User userUpdate)
    {
        var user = await _context.FindAsync<User>(userId);
        if (user == null)
            return new NotFoundResult();

        else if (user.EntityInfo.Deleted.HasValue)
            return new NotFoundResult();

        else if (userUpdate == null)
            return new BadRequestObjectResult("User information was not available.");

        // Update relevant user properties:
        user.UserName = userUpdate.UserName ?? user.UserName;
        user.FirstName = userUpdate.FirstName ?? user.FirstName;
        user.LastName = userUpdate.LastName ?? user.LastName;
        user.Email = userUpdate.Email ?? user.Email;
        user.PhoneNumber = userUpdate.PhoneNumber ?? user.PhoneNumber;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<IActionResult> CreateUserAsync(User user)
    {
        if (user == null)
            return new BadRequestObjectResult("User information was not available.");

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return new CreatedAtRouteResult("GetUser", new { UserId = user.UserId }, user);
    }

    public async Task<IActionResult> DeleteUserAsync(int userId, string loggedUser)
    {
        var user = await _context.FindAsync<User>(userId);

        if (user == null)
            return new NotFoundResult();

        else if (user.EntityInfo.Deleted.HasValue)
            return new StatusCodeResult((int)HttpStatusCode.Gone);

        user.EntityInfo.Deleted = DateTime.UtcNow;

        // TO DO: Implement user who made GetHttpContextExtensions.GetHttpContext().query/logged. Same to Interceptor
        user.EntityInfo.DeleteBy = loggedUser;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new OkResult();
    }
}