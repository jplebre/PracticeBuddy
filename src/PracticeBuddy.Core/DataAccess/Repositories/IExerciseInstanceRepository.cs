using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public interface IExerciseInstanceRepository
{
    Task<bool> DeleteExerciseInstance(int exerciseInstanceId);
    Task<ExerciseInstance> GetExerciseInstance(int exerciseInstanceId);
    Task<List<ExerciseInstance>> GetExerciseInstancesByExercise(int userId);
    Task<List<ExerciseInstance>> GetExerciseInstancesByRoutine(int routineId);
    Task<List<ExerciseInstance>> GetExerciseInstancesByUser(int userId);
    Task<bool> IncreaseExerciseInstancePracticeCount(int exerciseInstanceId);
    Task<int> InsertExerciseInstance(List<ExerciseInstance> exerciseInstances);
    Task<int> UpdateExerciseInstance(List<ExerciseInstance> exerciseInstances);
}
