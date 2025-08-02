namespace PracticeBuddy.Core.Constants;

public class Key
{
    public Modes Mode { get; init; }
    public MusicalNotes Tonic { get; init; }

    public Key(MusicalNotes tonic, Modes mode)
    {
        Mode = mode;
        Tonic = tonic;
        // MinorKeys.Contains(Note, Mode)?
    }
    
    public override string ToString()
    {
        return $"{Tonic} {Mode}";
    }
}