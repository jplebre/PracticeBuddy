namespace PracticeBuddy.Core.DataModels;

public class PracticeSuggestionResponse
{
    public DateTimeOffset DateRequested { get; set; }
    public TimeSpan PracticeDuration { get; set; }
    public List<ExerciseSuggestion> ExerciseSuggestions { get; set; }
}
