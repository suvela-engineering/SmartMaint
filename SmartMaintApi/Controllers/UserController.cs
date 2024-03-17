using System.Net;
using Microsoft.AspNetCore.Mvc;
using SmartMaintApi.Models;
using SmartMaintApi.Services;


[Route("API/[controller]")]
[ApiController]
[ProducesResponseType(statusCode: 400, type: typeof(void))] // 400 - Bad request
[ProducesResponseType(statusCode: 404, type: typeof(void))] // 404 - Not Found
[ProducesResponseType(statusCode: 500, type: typeof(void))] // 500 - Internal server error
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // **Read User**
    [HttpGet("{userId}", Name = "GetUser")]
    [ProducesResponseType(statusCode: 200, type: typeof(void))] // 200 - Success
    public async Task<IActionResult> GetUser(int userId)
    {
        try
        {
            return await _userService.GetUserAsync(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error get user: {ex.Message}");
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // **Read All Users**
    [HttpGet(Name = "GetAllUsers")]
    [ProducesResponseType(statusCode: 200, type: typeof(void))] // 200 - Success
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all users: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    // **Create User** (assuming password is handled separately)
    [HttpPost]
    [ProducesResponseType(statusCode: 201, type: typeof(void))] // 201 - Created at route result
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            return await _userService.CreateUserAsync(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // **Update User**
    [ProducesResponseType(statusCode: 204, type: typeof(void))] // 204 - No Content: Ok
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] User userUpdate)
    {
        try
        {
            return await _userService.UpdateUserAsync(userId, userUpdate);
        }
        catch (Exception ex)
        {
            // Handle exceptions here (e.g., log the error, return specific error response)
            Console.WriteLine($"Error updating user: {ex.Message}");

            return StatusCode(500, "An error occurred while updating the user.");
        }
    }
    // **Delete User** (soft delete with audit trail)
    [HttpDelete("{userId}")]
    // [ProducesResponseType(statusCode: 204, type: typeof(void))]
    [ProducesResponseType(statusCode: 200, type: typeof(void))] // 204 - No Content: Ok
    [ProducesResponseType(statusCode: 410, type: typeof(void))] // 410 - Client Error (Resource Gone)
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            // TO DO: Implement user who made the query/logged. Same to Interceptor
            string loggedUser = HttpContext?.User?.Identity?.Name ?? "Unknown User";

            return await _userService.DeleteUserAsync(userId, loggedUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}