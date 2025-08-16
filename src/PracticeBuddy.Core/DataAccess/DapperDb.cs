using Dapper;
using MySqlConnector;
using PracticeBuddy.Core.DataAccess.TypeMappers;

namespace PracticeBuddy.Core.DataAccess;

public class DapperDb : IDapperDb
{
    private readonly MySqlConnection _connection;

    public DapperDb(MySqlConnection connection)
    {
        _connection = connection;
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        SqlMapper.AddTypeHandler(typeof(TimeSpan), TimeSpanHandler.Default);
    }

    public async Task<bool> PingAsync()
    {
        return await _connection.QuerySingleAsync<bool>("SELECT 1;");
    }

    public async Task<T> QuerySingleAsync<T>(string sql)
    {
        try
        {
            return await _connection.QuerySingleAsync<T>(sql);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<T> QuerySingleAsync<T>(string sql, dynamic parameters)
    {
        try
        {
            return await _connection.QuerySingleAsync<T>(sql, parameters as object);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
    {
        try
        {
            return await _connection.QueryAsync<T>(sql);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic parameters)
    {
        try
        {
            return await _connection.QueryAsync<T>(sql, parameters as object);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> ExecuteAsync(string sql)
    {
        try
        {
            return await _connection.ExecuteAsync(sql);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> ExecuteAsync(string sql, dynamic parameters)
    {
        try
        {
            return await _connection.ExecuteAsync(sql, parameters as object);
        }
        catch (Exception)
        {
            throw;
        }
    }
}