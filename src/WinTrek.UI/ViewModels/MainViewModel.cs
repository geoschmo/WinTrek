using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinTrek.Core.Enums;
using WinTrek.Core.Events;
using WinTrek.Core.Models;
using WinTrek.Core.Services;

namespace WinTrek.UI.ViewModels;

/// <summary>
/// Main view model for the game window.
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    private readonly IGameEngine _engine;

    public MainViewModel() : this(new GameEngine())
    {
    }

    public MainViewModel(IGameEngine engine)
    {
        _engine = engine;

        // Subscribe to engine events
        _engine.MessageLogged += OnMessageLogged;
        _engine.StateChanged += OnStateChanged;
        _engine.GameOver += OnGameOver;

        // Start a new game
        _engine.StartNewGame();
        RefreshAllProperties();
    }

    #region Game State Properties

    [ObservableProperty]
    private string _quadrantName = string.Empty;

    [ObservableProperty]
    private string _quadrantPosition = string.Empty;

    [ObservableProperty]
    private string _sectorPosition = string.Empty;

    [ObservableProperty]
    private int _stardate;

    [ObservableProperty]
    private int _timeRemaining;

    [ObservableProperty]
    private int _energy;

    [ObservableProperty]
    private int _shieldLevel;

    [ObservableProperty]
    private int _photonTorpedoes;

    [ObservableProperty]
    private string _condition = "GREEN";

    [ObservableProperty]
    private bool _isDocked;

    [ObservableProperty]
    private int _totalKlingons;

    [ObservableProperty]
    private int _totalStarbases;

    [ObservableProperty]
    private bool _isGameActive;

    [ObservableProperty]
    private string _gameOverMessage = string.Empty;

    #endregion

    #region Input Properties

    [ObservableProperty]
    private string _navigationDirection = "1";

    [ObservableProperty]
    private string _navigationWarpFactor = "1";

    [ObservableProperty]
    private string _phaserEnergy = "100";

    [ObservableProperty]
    private string _torpedoDirection = "1";

    [ObservableProperty]
    private string _shieldTransfer = "100";

    #endregion

    #region Sector Grid

    [ObservableProperty]
    private ObservableCollection<SectorCellViewModel> _sectorGrid = new();

    #endregion

    #region Messages

    public ObservableCollection<GameMessageViewModel> Messages { get; } = new();

    #endregion

    #region Commands

    [RelayCommand]
    private void NewGame()
    {
        Messages.Clear();
        _engine.StartNewGame();
        RefreshAllProperties();
    }

    [RelayCommand]
    private void SaveGame()
    {
        var dialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "WinTrek Save Files (*.wtrek)|*.wtrek",
            DefaultExt = ".wtrek",
            Title = "Save Game"
        };

        if (dialog.ShowDialog() == true)
        {
            _engine.SaveGame(dialog.FileName);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void LoadGame()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "WinTrek Save Files (*.wtrek)|*.wtrek",
            DefaultExt = ".wtrek",
            Title = "Load Game"
        };

        if (dialog.ShowDialog() == true)
        {
            Messages.Clear();
            _engine.LoadGame(dialog.FileName);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void Navigate()
    {
        if (double.TryParse(NavigationDirection, out double direction) &&
            double.TryParse(NavigationWarpFactor, out double warpFactor))
        {
            _engine.Navigate(direction, warpFactor);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void FirePhasers()
    {
        if (int.TryParse(PhaserEnergy, out int energy))
        {
            _engine.FirePhasers(energy);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void FireTorpedo()
    {
        if (double.TryParse(TorpedoDirection, out double direction))
        {
            _engine.FireTorpedo(direction);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void AddShields()
    {
        if (int.TryParse(ShieldTransfer, out int amount))
        {
            _engine.TransferShields(amount);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void RemoveShields()
    {
        if (int.TryParse(ShieldTransfer, out int amount))
        {
            _engine.TransferShields(-amount);
            RefreshAllProperties();
        }
    }

    [RelayCommand]
    private void ShortRangeScan()
    {
        _engine.ShortRangeScan();
        RefreshAllProperties();
    }

    [RelayCommand]
    private void LongRangeScan()
    {
        var result = _engine.LongRangeScan();
        RefreshAllProperties();

        if (result.Success)
        {
            var dialog = new Views.LongRangeScanWindow(result);
            dialog.Owner = System.Windows.Application.Current.MainWindow;
            dialog.ShowDialog();
        }
    }

    [RelayCommand]
    private void ViewGalacticRecord()
    {
        var result = _engine.GetGalacticRecord();

        if (result.Success)
        {
            var dialog = new Views.GalacticRecordWindow(result);
            dialog.Owner = System.Windows.Application.Current.MainWindow;
            dialog.ShowDialog();
        }
    }

    [RelayCommand]
    private void ViewStatusReport()
    {
        var result = _engine.GetSystemStatus();
        var dialog = new Views.StatusReportWindow(result);
        dialog.Owner = System.Windows.Application.Current.MainWindow;
        dialog.ShowDialog();
    }

    [RelayCommand]
    private void CalculateTorpedoSolutions()
    {
        var solutions = _engine.CalculateTorpedoSolutions();
        foreach (var solution in solutions)
        {
            AddMessage($"Direction {solution.Direction:F2}: Klingon at {solution.TargetPosition.ToDisplayString()}", MessageType.Info);
        }
    }

    #endregion

    #region Private Methods

    private void RefreshAllProperties()
    {
        // Game state
        Stardate = _engine.State.Stardate;
        TimeRemaining = _engine.State.TimeRemaining;
        IsGameActive = _engine.State.IsGameActive;

        // Enterprise
        Energy = _engine.Enterprise.Energy;
        ShieldLevel = _engine.Enterprise.ShieldLevel;
        PhotonTorpedoes = _engine.Enterprise.PhotonTorpedoes;
        IsDocked = _engine.Enterprise.IsDocked;
        QuadrantPosition = _engine.Enterprise.QuadrantPosition.ToDisplayString();
        SectorPosition = _engine.Enterprise.SectorPosition.ToDisplayString();

        // Current quadrant
        QuadrantName = _engine.CurrentQuadrant.Name;
        Condition = _engine.Enterprise.GetCondition(_engine.CurrentQuadrant.KlingonCount).ToString().ToUpper();

        // Galaxy totals
        TotalKlingons = _engine.Galaxy.TotalKlingons;
        TotalStarbases = _engine.Galaxy.TotalStarbases;

        // Refresh sector grid
        RefreshSectorGrid();
    }

    private void RefreshSectorGrid()
    {
        SectorGrid.Clear();

        var scanResult = _engine.ShortRangeScan();
        if (scanResult.Success && scanResult.SectorGrid != null)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    SectorGrid.Add(new SectorCellViewModel
                    {
                        X = x,
                        Y = y,
                        Content = scanResult.SectorGrid[y, x]
                    });
                }
            }
        }
    }

    private void OnMessageLogged(object? sender, GameMessageEventArgs e)
    {
        AddMessage(e.Message, e.Type);
    }

    private void AddMessage(string message, MessageType type)
    {
        // Add to beginning so newest messages appear at top
        Messages.Insert(0, new GameMessageViewModel
        {
            Message = message,
            Type = type,
            Timestamp = DateTime.Now
        });

        // Keep only last 100 messages
        while (Messages.Count > 100)
        {
            Messages.RemoveAt(Messages.Count - 1);
        }
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        RefreshAllProperties();
    }

    private void OnGameOver(object? sender, GameOverEventArgs e)
    {
        GameOverMessage = e.Message;
        RefreshAllProperties();
    }

    #endregion
}

/// <summary>
/// View model for a single sector cell.
/// </summary>
public class SectorCellViewModel
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
/// View model for a game message.
/// </summary>
public class GameMessageViewModel
{
    public string Message { get; set; } = string.Empty;
    public MessageType Type { get; set; }
    public DateTime Timestamp { get; set; }
}
