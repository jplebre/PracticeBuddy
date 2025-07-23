namespace PracticeBuddy.Core.Models;

public class Routine
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Exercise> Exercises { get; set; }
    public DateTimeOffset LastPracticedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastUpdatedAt { get; set; }
}