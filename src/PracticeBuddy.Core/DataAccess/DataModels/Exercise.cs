namespace PracticeBuddy.Core.DataModels;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GoalBpm { get; set; }
    public int PracticeCount { get; set; }

    // TODO: Go the JSON route for these two
    // Or maybe even better - move them down to the exercise_isntance?
    // public Dictionary<Key, bool> EnabledKeys { get; set; }
    // public Dictionary<Modifier, bool> EnabledModifiers { get; set; }
    public int RoutineId { get; set; }
    public int UserId { get; set; }
    public DateTime? LastPracticedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
