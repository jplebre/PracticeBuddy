namespace PracticeBuddy.Core.DataAccess.DataModels;

// TODO: NOT a data model (this is a contract for the suggestion API)

public class PracticeSuggestionResponse
{
    public DateTimeOffset DateRequested { get; set; }
    public TimeSpan PracticeDuration { get; set; }
    public List<ExerciseSuggestion> ExerciseSuggestions { get; set; }
}
