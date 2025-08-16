using System.Data;
using Dapper;
using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public class RoutineRepository : IRoutineRepository
{
    private readonly IDapperDb _database;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RoutineRepository(IDapperDb database, IDateTimeProvider dateTimeProvider)
    {
        _database = database;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<int> InsertRoutine(Routine routine)
    {
        var query = @"
            INSERT INTO `routine` 
            (name,user_id,practice_count,last_practiced_at,created_at,last_updated_at)
            VALUES 
            (@Name,@UserId,@PracticeCount,@LastPracticedAt,@CreatedAt,@LastUpdatedAt);
            SELECT LAST_INSERT_ID()";

        routine.CreatedAt = _dateTimeProvider.Now();
        routine.LastUpdatedAt = _dateTimeProvider.Now();
        routine.LastPracticedAt = null;
        routine.PracticeCount = 0;

        if (!routine.Exercises.Any())
            return await _database.QuerySingleAsync<int>(query, routine);

        // not a complete implementation, only here for TDD purposes
        // refactor into correct repository
        var routineId = await _database.QuerySingleAsync<int>(query, routine);
        routine.Exercises = routine.Exercises.Select(e =>
            {
                e.UserId = routine.UserId;
                e.RoutineId = routineId;
                e.CreatedAt = _dateTimeProvider.Now();
                e.LastUpdatedAt = _dateTimeProvider.Now();
                e.LastPracticedAt = null;
                e.PracticeCount = 0;
                return e;
            }).ToList();

        string processQuery = @"
            INSERT INTO `exercise`
            (name,goal_bpm,practice_count,exercise_duration,routine_id,user_id,last_practiced_at,created_at,last_updated_at)
            VALUES (@Name, @GoalBpm, @PracticeCount, @ExerciseDuration, @RoutineId, @UserId, @LastPracticedAt, @CreatedAt, @LastUpdatedAt)";
        var affectedRows = await _database.ExecuteAsync(processQuery, routine.Exercises);
        return routineId;
    }

    public async Task<Routine> GetRoutine(int routineId)
    {
        var query = @"SELECT * FROM `routine` WHERE id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", routineId);

        var routine = await _database.QuerySingleAsync<Routine>(query, parameters);

        var exerciseQuery = @"SELECT * FROM `exercise` WHERE routine_id = @Id";
        routine.Exercises = (await _database.QueryAsync<Exercise>(exerciseQuery, parameters)).ToList();

        return routine;
    }

    public async Task<IList<Routine>> GetRoutines()
    {
        var query = "SELECT * FROM `routine`";
        return (await _database.QueryAsync<Routine>(query)).ToList();
    }
}