using Microsoft.AspNetCore.Mvc;
using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.API.Controllers;

/// <summary>
/// A Routine is a collection of exercises musicians practice every day.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoutinesController : Controller
{
    // Gets the user per context (and role), so only returns for user
    // Admins could get for eveyone, and then use filters `?UserId={userId}`

    /// <summary>
    /// Gets all routines created by current user
    /// </summary>
    /// <returns>All routines created by current user</returns>
    /// <response code="200">Returns the newly created Routine</response>
    /// <response code="403">If the User is not recognized or not allowed to create routines</response>
    [HttpGet] // Admin only
    public async Task<IActionResult> GetAllRoutines()
    {
        // Gets a specific routine created by this user, using ID
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a new routine for the current user
    /// </summary>
    /// <param name="item"></param>
    /// <returns>A newly created Routine</returns>
    /// <response code="201">Returns the newly created Routine</response>
    /// <response code="400">If the Routine is invalid</response>
    /// <response code="403">If the User is not recognized or not allowed to create routines</response>
    [HttpPost]
    public async Task<IActionResult> PostRoutine([FromBody]Routine body)
    {
        // Creates a routine. Gets user ID from context
        // redirects to /routine/{routineId}
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a specific routine, by Id
    /// </summary>
    /// <param name="routineId"></param>
    /// <returns>A newly created Routine</returns>
    /// <response code="200">Returns the specified routine</response>
    /// <response code="400">If the parameter is invalid</response>
    /// <response code="403">If the User is not recognized or not allowed to access this endpoint</response>
    /// <response code="404">If there's no such routine for the current user</response>
    [HttpGet("{routineId}")] // Also, PATCH, DELETE
    public async Task<IActionResult> GetRoutineById(int routineId)
    {
        // Gets a specific routine created by this user, using ID
        throw new NotImplementedException();
    }

    [HttpGet("{routineId}/exercises")] // User Authenticated.
    public async Task<IActionResult> GetAllExercisesForRoutineId(int routineId)
    {
        // Gets all exercises created for this routine
        throw new NotImplementedException();
    }

    [HttpPost("{routineId}/exercises")] // User Authenticated.
    public async Task<IActionResult> CreatesExerciseForRoutine(int routineId /*, FromBody body*/)
    {
        // Creates an exercise for this routine
        // This endpoint should be here (and not exercises) as exercises should be part of a routine
        // Maybe we can have orphaned exercises, but havent' thought of it.
        // Redirects to /exercises/{exerciseId}
        throw new NotImplementedException();
    }

    [HttpGet("{routineId}/timeline")] // GET only
    public async Task<IActionResult> GetAllRecordedSessionsForExercise(int routineId)
    {
        // Gets all recorded practice sessions for a specific exercise
        throw new NotImplementedException();
    }
}