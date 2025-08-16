using PracticeBuddy.Core.Constants;

namespace PracticeBuddy.Core.DataAccess.DataModels;

public class ExerciseInstance
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tonality { get; set; } // "Key" is a reserved MySql Keyword
    public string Modifier { get; set; }
    public TimeSpan ExerciseDuration { get; set; } = new TimeSpan(0,5,0);
    public TimeSpan TotalTimePracticed { get; set; } = TimeSpan.Zero;
    public int GoalBpm { get; set; }
    public int PracticeCount { get; set; }
    public int ExerciseId { get; set; }             // FK
    public int UserId { get; set; }                 // FK    
    public DateTime? LastPracticedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}