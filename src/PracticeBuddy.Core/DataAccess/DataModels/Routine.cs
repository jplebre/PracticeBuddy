namespace PracticeBuddy.Core.DataAccess.DataModels;

public class Routine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public int PracticeCount { get; set; }
    public List<Exercise> Exercises { get; set; }
    public DateTime? LastPracticedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}