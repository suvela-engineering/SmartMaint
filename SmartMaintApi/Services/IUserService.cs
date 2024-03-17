using System.Collections;
using Microsoft.AspNetCore.Mvc;
using SmartMaintApi.Models;

namespace SmartMaintApi.Services
{
    public interface IUserService
    {
        Task<IActionResult> GetUserAsync(int userId);
        Task<IEnumerable> GetAllUsersAsync();
        Task<IActionResult> CreateUserAsync(User user);
        Task<IActionResult> UpdateUserAsync(int userId, User user);
        Task<IActionResult> DeleteUserAsync(int userId, string loggedUser);
    }
}