namespace WinTrek.Core.Models;

/// <summary>
/// Holds the current state of the game.
/// </summary>
public class GameState
{
    /// <summary>
    /// Current stardate.
    /// </summary>
    public int Stardate { get; set; }

    /// <summary>
    /// Time units remaining to complete the mission.
    /// </summary>
    public int TimeRemaining { get; set; }

    /// <summary>
    /// Initial number of Klingons at game start (for scoring).
    /// </summary>
    public int InitialKlingons { get; set; }

    /// <summary>
    /// Initial number of starbases at game start.
    /// </summary>
    public int InitialStarbases { get; set; }

    /// <summary>
    /// Whether the game is currently active.
    /// </summary>
    public bool IsGameActive { get; set; }

    /// <summary>
    /// Whether the game has been won.
    /// </summary>
    public bool IsVictory { get; set; }

    /// <summary>
    /// Resets the game state for a new game.
    /// </summary>
    public void Reset(Random random)
    {
        Stardate = random.Next(50) + 2250;
        TimeRemaining = 40 + random.Next(10);
        IsGameActive = true;
        IsVictory = false;
    }

    /// <summary>
    /// Advances time by one unit (called when changing quadrants).
    /// </summary>
    public void AdvanceTime()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining--;
            Stardate++;
        }
    }
}
