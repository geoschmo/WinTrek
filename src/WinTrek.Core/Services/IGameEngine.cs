using WinTrek.Core.Events;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Main game engine interface that the UI layer consumes.
/// </summary>
public interface IGameEngine
{
    #region State Properties

    /// <summary>
    /// Current game state (stardate, time remaining, etc.).
    /// </summary>
    GameState State { get; }

    /// <summary>
    /// The player's ship.
    /// </summary>
    Enterprise Enterprise { get; }

    /// <summary>
    /// The galaxy containing all quadrants.
    /// </summary>
    Galaxy Galaxy { get; }

    /// <summary>
    /// The current quadrant the Enterprise is in.
    /// </summary>
    Quadrant CurrentQuadrant { get; }

    /// <summary>
    /// Klingon ships in the current quadrant.
    /// </summary>
    IReadOnlyList<KlingonShip> KlingonShips { get; }

    #endregion

    #region Events

    /// <summary>
    /// Fired when a game message should be displayed.
    /// </summary>
    event EventHandler<GameMessageEventArgs>? MessageLogged;

    /// <summary>
    /// Fired when the game state changes (for UI refresh).
    /// </summary>
    event EventHandler? StateChanged;

    /// <summary>
    /// Fired when the game ends.
    /// </summary>
    event EventHandler<GameOverEventArgs>? GameOver;

    #endregion

    #region Game Commands

    /// <summary>
    /// Starts a new game.
    /// </summary>
    void StartNewGame();

    /// <summary>
    /// Navigates the Enterprise.
    /// </summary>
    /// <param name="direction">Course direction (1.0 - 9.0).</param>
    /// <param name="warpFactor">Warp speed (0.1 - 8.0).</param>
    /// <returns>Result of the navigation attempt.</returns>
    NavigationResult Navigate(double direction, double warpFactor);

    /// <summary>
    /// Fires phasers at all Klingons in the current quadrant.
    /// </summary>
    /// <param name="energy">Energy to use for phasers.</param>
    /// <returns>Result of the phaser attack.</returns>
    PhaserResult FirePhasers(int energy);

    /// <summary>
    /// Fires a photon torpedo.
    /// </summary>
    /// <param name="direction">Firing direction (1.0 - 9.0).</param>
    /// <returns>Result of the torpedo attack.</returns>
    TorpedoResult FireTorpedo(double direction);

    /// <summary>
    /// Transfers energy to shields.
    /// </summary>
    /// <param name="amount">Amount to transfer (positive = add, negative = remove).</param>
    /// <returns>True if transfer was successful.</returns>
    bool TransferShields(int amount);

    /// <summary>
    /// Performs a short-range scan.
    /// </summary>
    ShortRangeScanResult ShortRangeScan();

    /// <summary>
    /// Performs a long-range scan.
    /// </summary>
    LongRangeScanResult LongRangeScan();

    /// <summary>
    /// Gets the galactic record.
    /// </summary>
    GalacticRecordResult GetGalacticRecord();

    /// <summary>
    /// Gets the current system status.
    /// </summary>
    SystemStatusResult GetSystemStatus();

    /// <summary>
    /// Calculates torpedo firing solution for all Klingons.
    /// </summary>
    IReadOnlyList<TorpedoSolution> CalculateTorpedoSolutions();

    /// <summary>
    /// Calculates navigation solution to a quadrant.
    /// </summary>
    NavigationSolution CalculateNavigationTo(Position targetQuadrant);

    /// <summary>
    /// Calculates navigation solution to the starbase in current quadrant.
    /// </summary>
    NavigationSolution? CalculateStarbaseSolution();

    #endregion
}

/// <summary>
/// Torpedo firing solution for a target.
/// </summary>
public class TorpedoSolution
{
    public Position TargetPosition { get; init; }
    public double Direction { get; init; }
    public double Distance { get; init; }
}

/// <summary>
/// Navigation solution to a destination.
/// </summary>
public class NavigationSolution
{
    public Position TargetPosition { get; init; }
    public double Direction { get; init; }
    public double Distance { get; init; }
}
