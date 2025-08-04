using PracticeBuddy.Core.DataModels;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public interface IExerciseRepository
{
    Task<Exercise> GetExercise(int routineId);
    Task<IList<Exercise>> GetExercises();
    Task<int> InsertExercise(Exercise exercise);
}
