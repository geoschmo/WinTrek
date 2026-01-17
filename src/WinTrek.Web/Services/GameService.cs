using WinTrek.Core.Enums;
using WinTrek.Core.Events;
using WinTrek.Core.Models;
using WinTrek.Core.Services;

namespace WinTrek.Web.Services;

/// <summary>
/// Service that wraps IGameEngine for Blazor components.
/// Manages game state and exposes events for UI updates.
/// </summary>
public class GameService : IDisposable
{
    private readonly IGameEngine _engine;
    private readonly List<GameMessage> _messages = new();
    private const int MaxMessages = 100;

    public GameService(IGameEngine engine)
    {
        _engine = engine;

        // Subscribe to engine events
        _engine.MessageLogged += OnMessageLogged;
        _engine.StateChanged += OnStateChanged;
        _engine.GameOver += OnGameOver;

        // Start a new game
        _engine.StartNewGame();
    }

    #region Events for Blazor Components

    /// <summary>
    /// Fired when the game state changes and UI should refresh.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Fired when the game ends.
    /// </summary>
    public event Action<string>? OnGameEnded;

    #endregion

    #region Game State Properties

    public string QuadrantName => _engine.CurrentQuadrant.Name;
    public string QuadrantPosition => _engine.Enterprise.QuadrantPosition.ToDisplayString();
    public string SectorPosition => _engine.Enterprise.SectorPosition.ToDisplayString();
    public int Stardate => _engine.State.Stardate;
    public int TimeRemaining => _engine.State.TimeRemaining;
    public int Energy => _engine.Enterprise.Energy;
    public int ShieldLevel => _engine.Enterprise.ShieldLevel;
    public int PhotonTorpedoes => _engine.Enterprise.PhotonTorpedoes;
    public string Condition => _engine.Enterprise.GetCondition(_engine.CurrentQuadrant.KlingonCount).ToString().ToUpper();
    public bool IsDocked => _engine.Enterprise.IsDocked;
    public int TotalKlingons => _engine.Galaxy.TotalKlingons;
    public int TotalStarbases => _engine.Galaxy.TotalStarbases;
    public bool IsGameActive => _engine.State.IsGameActive;

    public IReadOnlyList<GameMessage> Messages => _messages;

    #endregion

    #region Sector Grid

    public SectorCell[,] GetSectorGrid()
    {
        var grid = new SectorCell[8, 8];
        var quadrant = _engine.CurrentQuadrant;

        // Read directly from CurrentQuadrant.Sectors to avoid triggering events
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                grid[y, x] = new SectorCell
                {
                    X = x,
                    Y = y,
                    Content = quadrant.Sectors[y, x]
                };
            }
        }

        return grid;
    }

    #endregion

    #region Game Commands

    public void NewGame()
    {
        _messages.Clear();
        _engine.StartNewGame();
        NotifyStateChanged();
    }

    public void Navigate(double direction, double warpFactor)
    {
        _engine.Navigate(direction, warpFactor);
        NotifyStateChanged();
    }

    public void FirePhasers(int energy)
    {
        _engine.FirePhasers(energy);
        NotifyStateChanged();
    }

    public void FireTorpedo(double direction)
    {
        _engine.FireTorpedo(direction);
        NotifyStateChanged();
    }

    public void TransferShields(int amount)
    {
        _engine.TransferShields(amount);
        NotifyStateChanged();
    }

    public void ShortRangeScan()
    {
        _engine.ShortRangeScan();
        NotifyStateChanged();
    }

    public LongRangeScanResult LongRangeScan()
    {
        var result = _engine.LongRangeScan();
        NotifyStateChanged();
        return result;
    }

    public GalacticRecordResult GetGalacticRecord()
    {
        return _engine.GetGalacticRecord();
    }

    public SystemStatusResult GetSystemStatus()
    {
        return _engine.GetSystemStatus();
    }

    public IReadOnlyList<TorpedoSolution> CalculateTorpedoSolutions()
    {
        var solutions = _engine.CalculateTorpedoSolutions();
        foreach (var solution in solutions)
        {
            AddMessage($"Direction {solution.Direction:F2}: Klingon at {solution.TargetPosition.ToDisplayString()}", MessageType.Info);
        }
        NotifyStateChanged();
        return solutions;
    }

    #endregion

    #region Save/Load (for localStorage in browser)

    /// <summary>
    /// Gets the current game state as JSON for saving to localStorage.
    /// </summary>
    public string GetSaveDataJson()
    {
        var saveService = new GameSaveService();
        var saveData = saveService.CreateSaveData(
            _engine.State,
            _engine.Enterprise,
            _engine.Galaxy,
            _engine.KlingonShips);

        return System.Text.Json.JsonSerializer.Serialize(saveData);
    }

    /// <summary>
    /// Loads game state from JSON (from localStorage).
    /// </summary>
    public bool LoadFromJson(string json)
    {
        try
        {
            var saveData = System.Text.Json.JsonSerializer.Deserialize<GameSaveData>(json);
            if (saveData == null)
                return false;

            // We need to recreate the engine state from save data
            // For now, create a temporary file and use the engine's LoadGame method
            var tempPath = Path.GetTempFileName();
            try
            {
                File.WriteAllText(tempPath, json);
                _messages.Clear();
                _engine.LoadGame(tempPath);
                NotifyStateChanged();
                return true;
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Private Methods

    private void OnMessageLogged(object? sender, GameMessageEventArgs e)
    {
        AddMessage(e.Message, e.Type);
    }

    private void AddMessage(string message, MessageType type)
    {
        _messages.Insert(0, new GameMessage
        {
            Text = message,
            Type = type,
            Timestamp = DateTime.Now
        });

        // Keep only last 100 messages
        while (_messages.Count > MaxMessages)
        {
            _messages.RemoveAt(_messages.Count - 1);
        }

        // Don't call NotifyStateChanged() here - it will be called by the parent method
        // or by OnStateChanged when the engine fires its StateChanged event
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        NotifyStateChanged();
    }

    private void OnGameOver(object? sender, GameOverEventArgs e)
    {
        OnGameEnded?.Invoke(e.Message);
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }

    public void Dispose()
    {
        _engine.MessageLogged -= OnMessageLogged;
        _engine.StateChanged -= OnStateChanged;
        _engine.GameOver -= OnGameOver;
    }

    #endregion
}

/// <summary>
/// Represents a cell in the sector grid.
/// </summary>
public class SectorCell
{
    public int X { get; set; }
    public int Y { get; set; }
    public SectorContent Content { get; set; }

    public string Symbol => Content switch
    {
        SectorContent.Empty => "   ",
        SectorContent.Enterprise => "<*>",
        SectorContent.Klingon => "+++",
        SectorContent.Star => " * ",
        SectorContent.Starbase => ">!<",
        _ => "   "
    };
}

/// <summary>
/// Represents a game message for display.
/// </summary>
public class GameMessage
{
    public string Text { get; set; } = string.Empty;
    public MessageType Type { get; set; }
    public DateTime Timestamp { get; set; }
}
