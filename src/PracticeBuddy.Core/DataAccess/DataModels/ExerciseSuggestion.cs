namespace PracticeBuddy.Core.DataAccess.DataModels;

// TODO: NOT a data model (this is a contract for the suggestion API)
public class ExerciseSuggestion
{
    public int ExerciseId { get; set; }
    public TimeSpan ExerciseDuration { get; set; }
    public string ExerciseName { get; set; }
    public string Key { get; set; }
    public string Modifiers { get; set; }
    public int StartBpm { get; set; }
}