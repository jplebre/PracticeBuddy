namespace PracticeBuddy.Core.DataModels;

public class ExerciseSuggestion
{
    public Guid ExerciseId { get; set; }
    public TimeSpan ExerciseDuration { get; set; }
    public string ExerciseName { get; set; }
    public string Key { get; set; }
    public string Modifiers { get; set; }
    public int StartBpm { get; set; }
}