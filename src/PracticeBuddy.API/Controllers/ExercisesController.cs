
using Microsoft.AspNetCore.Mvc;

namespace PracticeBuddy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllExercises()
    {
        // Admin: Gets all exercises
        // User: gets his exercises (userId from context)
        throw new NotImplementedException();
    }

    [HttpGet("{exerciseId}")] // Also, PUT, PATCH, DELETE
    public async Task<IActionResult> GetExerciseById(int exerciseId)
    {
        // Gets a specific exercise by exercise ID
        throw new NotImplementedException();
    }

    [HttpGet("{exerciseId}/exercise-instances")] // GET only?
    public async Task<IActionResult> GetAllExerciseInstancesForExercise(int exerciseId)
    {
        // exercises-instances should be created/modified by parent exercise
        throw new NotImplementedException();
    }

    [HttpGet("{exerciseId}/timeline")] // GET only
    public async Task<IActionResult> GetAllRecordedSessionsForExercise(int exerciseId)
    {
        // Gets all recorded practice sessions for a specific exercise
        throw new NotImplementedException();
    }
}