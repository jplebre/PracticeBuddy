using PracticeBuddy.Core.Constants;

namespace PracticeBuddy.Core.DataAccess.DataModels;

public class Practice
{
    public int Id { get; set; }
    public int ComfortableBPM { get; set; }
    public int MaxBPMAttempted { get; set; }
    public SatisfactionLevel SatisfactionLevel { get; set; }
    public DateTimeOffset PracticedAt { get; set; }
    public string Notes { get; set; }
}