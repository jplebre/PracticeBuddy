using Dapper;
using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public class ExerciseInstanceRepository : IExerciseInstanceRepository
{
    private readonly IDapperDb _database;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ExerciseInstanceRepository(IDapperDb database, IDateTimeProvider dateTimeProvider)
    {
        _database = database;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<int> InsertExerciseInstance(List<ExerciseInstance> exerciseInstances)
    {
        var query = @"
            INSERT INTO `exercise_instance` 
            (name, tonality, modifier, exercise_duration, total_time_practiced, goal_bpm, practice_count, exercise_id, user_id, last_practiced_at, created_at, last_updated_at)
            VALUES 
            (@Name, @Tonality, @Modifier, @ExerciseDuration, @TotalTimePracticed, @GoalBpm, @PracticeCount, @ExerciseId, @UserId, @LastPracticedAt, @CreatedAt, @LastUpdatedAt)";

        exerciseInstances.Select(e =>
            {
                e.TotalTimePracticed = TimeSpan.Zero;
                e.PracticeCount = 0;
                e.LastPracticedAt = null;
                e.CreatedAt = _dateTimeProvider.Now();
                e.LastUpdatedAt = _dateTimeProvider.Now();
                return e;
            }).ToList();

        return await _database.ExecuteAsync(query, exerciseInstances);
    }

    public async Task<int> UpdateExerciseInstance(List<ExerciseInstance> exerciseInstances)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IncreaseExerciseInstancePracticeCount(int exerciseInstanceId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteExerciseInstance(int exerciseInstanceId)
    {
        throw new NotImplementedException();
    }

    public async Task<ExerciseInstance> GetExerciseInstance(int exerciseInstanceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ExerciseInstance>> GetExerciseInstancesByRoutine(int routineId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ExerciseInstance>> GetExerciseInstancesByUser(int userId)
    {
        var query = @"SELECT * FROM `exercise_instance` WHERE `user_id` = @UserId";
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);

        var exerciseInstances = await _database.QueryAsync<ExerciseInstance>(query, parameters);
        return exerciseInstances.ToList();
    }

    public async Task<List<ExerciseInstance>> GetExerciseInstancesByExercise(int userId)
    {
        throw new NotImplementedException();
    }
}