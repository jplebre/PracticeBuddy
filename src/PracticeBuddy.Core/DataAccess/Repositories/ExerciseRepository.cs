using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly IDapperDb _database;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ExerciseRepository(IDapperDb database, IDateTimeProvider dateTimeProvider)
    {
        _database = database;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<int> InsertExercise(Exercise exercise)
    {
        var query = @"
            INSERT INTO `exercise` 
            (name,user_id,routine_id,goal_bpm,practice_count,total_time_practiced,last_practiced_at,created_at,last_updated_at)
            VALUES 
            (@Name,@UserId,@RoutineId,@GoalBpm,@PracticeCount,@TotalTimePracticed,@LastPracticedAt,@CreatedAt,@LastUpdatedAt);
            SELECT LAST_INSERT_ID()";

        exercise.CreatedAt = _dateTimeProvider.Now();
        exercise.LastUpdatedAt = _dateTimeProvider.Now();
        exercise.LastPracticedAt = null;
        exercise.PracticeCount = 0;

        return await _database.QuerySingleAsync<int>(query, exercise);

        // // not a complete implementation, only here for TDD purposes
        // // refactor into correct repository
        // var routineId = await _database.QuerySingleAsync<int>(query, routine);
        // routine.Exercises = routine.Exercises.Select(e =>
        //     {
        //         e.UserId = routine.UserId;
        //         e.RoutineId = routineId;
        //         e.CreatedAt = _dateTimeProvider.Now();
        //         e.LastUpdatedAt = _dateTimeProvider.Now();
        //         e.LastPracticedAt = null;
        //         e.PracticeCount = 0;
        //         return e;
        //     }).ToList();

        // string processQuery = @"
        //     INSERT INTO `exercise`
        //     (name,goal_bpm,practice_count,routine_id,user_id,last_practiced_at,created_at,last_updated_at)
        //     VALUES (@Name, @GoalBpm, @PracticeCount, @RoutineId, @UserId, @LastPracticedAt, @CreatedAt, @LastUpdatedAt)";
        // var affectedRows = await _database.ExecuteAsync(processQuery, routine.Exercises);
        // return routineId;
    }

    public async Task<Exercise> GetExercise(int routineId)
    {
        throw new NotImplementedException();

        // var query = @"SELECT * FROM `routine` WHERE id = @Id";
        // var parameters = new DynamicParameters();
        // parameters.Add("@Id", routineId);

        // var routine = await _database.QuerySingleAsync<Routine>(query, parameters);

        // var exerciseQuery = @"SELECT * FROM `exercise` WHERE routine_id = @Id";
        // routine.Exercises = (await _database.QueryAsync<Exercise>(exerciseQuery, parameters)).ToList();

        // return routine;
    }

    public async Task<IList<Exercise>> GetExercises()
    {
        throw new NotImplementedException();
        // var query = "SELECT * FROM `routine`";
        // return (await _database.QueryAsync<Routine>(query)).ToList();
    }
}