namespace WinTrek.Core.Events;

/// <summary>
/// Event fired when the game ends.
/// </summary>
public class GameOverEventArgs : GameEventArgs
{
    public GameOutcome Outcome { get; }
    public int FinalScore { get; }
    public int KlingonsDestroyed { get; }
    public int StarbasesRemaining { get; }

    public GameOverEventArgs(string message, GameOutcome outcome, int score, int klingonsDestroyed, int starbases)
        : base(message)
    {
        Outcome = outcome;
        FinalScore = score;
        KlingonsDestroyed = klingonsDestroyed;
        StarbasesRemaining = starbases;
    }
}

/// <summary>
/// Possible game outcomes.
/// </summary>
public enum GameOutcome
{
    /// <summary>All Klingons destroyed - victory!</summary>
    Victory,

    /// <summary>Enterprise was destroyed.</summary>
    Destroyed,

    /// <summary>Ran out of energy.</summary>
    OutOfEnergy,

    /// <summary>Ran out of time.</summary>
    OutOfTime
}
