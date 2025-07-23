using PracticeBuddy.Core.Constants;

namespace PracticeBuddy.Core.Models;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int GoalBpm { get; set; }
    public Dictionary<Key, bool> EnabledKeys { get; set; }
    public Dictionary<Modifier, bool> EnabledModifiers { get; set; }
}
