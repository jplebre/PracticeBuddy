namespace PracticeBuddy.API.Infrastructure.Options;

public class PracticeBuddyApiOptions
{
    public MySqlOptions MySql { get; init; }
}

public class MySqlOptions
{
    public string ConnectionString { get; init; }
}