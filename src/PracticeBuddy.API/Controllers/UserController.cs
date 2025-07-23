using Microsoft.AspNetCore.Mvc;

namespace PracticeBuddy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ILogger<UserController> logger) : Controller
{
    // Probably don't need these endpoints yet. 
    // More for users, portal, searches, home pages, etc.

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        // Gets all Users
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        // Gets user by id
        throw new NotImplementedException();
    }

    [HttpGet("{userId}/active-routine")]
    public async Task<IActionResult> GetUserActiveRoutine(int userId)
    {
        // Gets user's currently active routine
        throw new NotImplementedException();
    }
}