using System.Data;
using Dapper;
using PracticeBuddy.Core.DataModels;

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

        return await _database.QuerySingleAsync<int>(query, routine);
    }

    public async Task<Routine> GetRoutine(int routineId)
    {
        var query = "SELECT * FROM `routine` WHERE id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", routineId);
        
        return await _database.QuerySingleAsync<Routine>(query, parameters);
    }

    public async Task<IList<Routine>> GetRoutines()
    {
        var query = "SELECT * FROM `routine`";
        return (await _database.QueryAsync<Routine>(query)).ToList();
    }
}