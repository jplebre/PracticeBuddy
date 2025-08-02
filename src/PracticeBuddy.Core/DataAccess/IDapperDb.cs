namespace PracticeBuddy.Core.DataAccess;

public interface IDapperDb
{
    Task<bool> PingAsync();
    Task<T> QuerySingleAsync<T>(string sql);
    Task<T> QuerySingleAsync<T>(string sql, dynamic parameters);
    Task<IEnumerable<T>> QueryAsync<T>(string sql);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic parameters);
    Task<int> ExecuteAsync(string sql);
    Task<int> ExecuteAsync(string sql, dynamic parameters);
}