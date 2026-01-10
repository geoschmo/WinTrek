using WinTrek.Core.Enums;
using WinTrek.Core.Events;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Main game engine that orchestrates all game systems.
/// </summary>
public class GameEngine : IGameEngine
{
    private readonly Random _random;
    private readonly NavigationService _navigation;
    private readonly CombatService _combat;
    private readonly ScannerService _scanner;
    private readonly DamageService _damage;

    private readonly List<KlingonShip> _klingonShips = new();
    private Position _starbasePosition;

    public GameState State { get; } = new();
    public Enterprise Enterprise { get; } = new();
    public Galaxy Galaxy { get; } = new();
    public Quadrant CurrentQuadrant => Galaxy[Enterprise.QuadrantPosition];
    public IReadOnlyList<KlingonShip> KlingonShips => _klingonShips;

    public event EventHandler<GameMessageEventArgs>? MessageLogged;
    public event EventHandler? StateChanged;
    public event EventHandler<GameOverEventArgs>? GameOver;

    public GameEngine() : this(new Random())
    {
    }

    public GameEngine(Random random)
    {
        _random = random;
        _navigation = new NavigationService(random);
        _combat = new CombatService(random);
        _scanner = new ScannerService();
        _damage = new DamageService(random);
    }

    #region Game Commands

    public void StartNewGame()
    {
        // Reset game state
        State.Reset(_random);

        // Reset Enterprise
        Enterprise.Reset();
        Enterprise.QuadrantPosition = new Position(_random.Next(8), _random.Next(8));
        Enterprise.SectorPosition = new Position(_random.Next(8), _random.Next(8));

        // Initialize galaxy
        int totalKlingons = 15 + _random.Next(6);
        int totalStarbases = 2 + _random.Next(3);
        State.InitialKlingons = totalKlingons;
        State.InitialStarbases = totalStarbases;

        Galaxy.Initialize(_random, GameData.QuadrantNames, totalKlingons, totalStarbases);

        // Generate the initial sector
        GenerateSector();

        // Log mission briefing
        LogMessage($"Mission: Destroy {Galaxy.TotalKlingons} Klingon ships in {State.TimeRemaining} stardates with {Galaxy.TotalStarbases} starbases.",
            MessageType.System);

        OnStateChanged();
    }

    public NavigationResult Navigate(double direction, double warpFactor)
    {
        if (!State.IsGameActive)
            return NavigationResult.Failed("Game is not active.");

        var startQuadrant = Enterprise.QuadrantPosition;

        var result = _navigation.Navigate(Enterprise, Galaxy, CurrentQuadrant, direction, warpFactor);

        if (!result.Success)
        {
            LogMessage(result.Message, MessageType.Warning);
            return result;
        }

        LogMessage("Warp engines engaged.", MessageType.System);

        if (result.HitObstacle)
        {
            LogMessage("Encountered obstacle within quadrant.", MessageType.Warning);
        }

        // Check if we changed quadrants
        if (result.QuadrantChanged)
        {
            State.AdvanceTime();
            GenerateSector();
            CheckTimeLimit();
        }

        // Check for docking
        if (NavigationService.IsDockingLocation(CurrentQuadrant, Enterprise.SectorPosition))
        {
            Enterprise.Dock();
            LogMessage("Lowering shields as part of docking sequence...", MessageType.System);
            LogMessage("Enterprise successfully docked with starbase.", MessageType.Success);
        }
        else
        {
            Enterprise.Undock();

            // If Klingons in quadrant and we didn't change quadrants, they attack
            if (_klingonShips.Count > 0 && !result.QuadrantChanged)
            {
                var attackResult = _combat.KlingonsAttack(Enterprise, _klingonShips);
                LogKlingonAttacks(attackResult);

                if (attackResult.EnterpriseDestroyed)
                {
                    EndGame(GameOutcome.Destroyed);
                    return result;
                }
            }
            else if (!result.QuadrantChanged)
            {
                // Try repair or induce damage
                var repairResult = _damage.TryRepairDamage(Enterprise);
                if (repairResult.RepairOccurred && repairResult.FullyRepaired)
                {
                    LogMessage(repairResult.Message!, MessageType.Success);
                }
                else
                {
                    var damageResult = _damage.TryInduceDamage(Enterprise);
                    if (damageResult.DamageOccurred)
                    {
                        LogMessage(damageResult.Message!, MessageType.Damage);
                    }
                }
            }
        }

        OnStateChanged();
        return result;
    }

    public PhaserResult FirePhasers(int energy)
    {
        if (!State.IsGameActive)
            return PhaserResult.Failed("Game is not active.");

        var result = _combat.FirePhasers(Enterprise, CurrentQuadrant, _klingonShips, energy);

        if (!result.Success)
        {
            LogMessage(result.Message, MessageType.Warning);
            return result;
        }

        LogMessage("Firing phasers...", MessageType.Alert);

        foreach (var hit in result.Hits)
        {
            if (hit.Destroyed)
            {
                LogMessage($"Klingon ship destroyed at sector {hit.TargetPosition.ToDisplayString()}.", MessageType.Success);
                Galaxy.KlingonDestroyed();
            }
            else
            {
                LogMessage($"Hit ship at sector {hit.TargetPosition.ToDisplayString()}. Klingon shields dropped to {hit.RemainingShields}.", MessageType.Info);
            }
        }

        // Check for victory
        if (Galaxy.TotalKlingons == 0)
        {
            EndGame(GameOutcome.Victory);
            return result;
        }

        // Surviving Klingons attack back
        if (_klingonShips.Count > 0)
        {
            var attackResult = _combat.KlingonsAttack(Enterprise, _klingonShips);
            LogKlingonAttacks(attackResult);

            if (attackResult.EnterpriseDestroyed)
            {
                EndGame(GameOutcome.Destroyed);
            }
        }

        OnStateChanged();
        return result;
    }

    public TorpedoResult FireTorpedo(double direction)
    {
        if (!State.IsGameActive)
            return TorpedoResult.Failed("Game is not active.");

        var result = _combat.FireTorpedo(Enterprise, CurrentQuadrant, _klingonShips, direction);

        if (!result.Success)
        {
            LogMessage(result.Message, MessageType.Warning);
            return result;
        }

        LogMessage("Photon torpedo fired...", MessageType.Alert);

        // Log trajectory
        foreach (var pos in result.Path)
        {
            LogMessage($"  {pos.ToDisplayString()}", MessageType.Info);
        }

        // Log result
        switch (result.HitType)
        {
            case TorpedoHitType.Klingon:
                LogMessage(result.Message, MessageType.Success);
                Galaxy.KlingonDestroyed();
                break;
            case TorpedoHitType.Starbase:
                LogMessage(result.Message, MessageType.Damage);
                Galaxy.StarbaseDestroyed();
                break;
            case TorpedoHitType.Star:
            case TorpedoHitType.Miss:
                LogMessage(result.Message, MessageType.Info);
                break;
        }

        // Check for victory
        if (Galaxy.TotalKlingons == 0)
        {
            EndGame(GameOutcome.Victory);
            return result;
        }

        // Surviving Klingons attack back
        if (_klingonShips.Count > 0)
        {
            var attackResult = _combat.KlingonsAttack(Enterprise, _klingonShips);
            LogKlingonAttacks(attackResult);

            if (attackResult.EnterpriseDestroyed)
            {
                EndGame(GameOutcome.Destroyed);
            }
        }

        OnStateChanged();
        return result;
    }

    public bool TransferShields(int amount)
    {
        if (Enterprise.Damage.IsDamaged(ShipSystem.ShieldControl))
        {
            LogMessage("Shield controls are damaged. Repairs are underway.", MessageType.Warning);
            return false;
        }

        bool success = Enterprise.TransferShields(amount);

        if (success)
        {
            LogMessage($"Shield strength is now {Enterprise.ShieldLevel}. Energy level is now {Enterprise.Energy}.", MessageType.System);
            OnStateChanged();
        }
        else
        {
            LogMessage("Invalid amount of energy.", MessageType.Warning);
        }

        return success;
    }

    public ShortRangeScanResult ShortRangeScan()
    {
        var result = _scanner.ShortRangeScan(Enterprise, CurrentQuadrant);

        if (!result.Success)
        {
            LogMessage(result.Message, MessageType.Warning);
        }
        else if (result.KlingonCount > 0)
        {
            string plural = result.KlingonCount == 1 ? "" : "s";
            LogMessage($"Condition RED: Klingon ship{plural} detected.", MessageType.Alert);

            if (Enterprise.ShieldLevel == 0 && !Enterprise.IsDocked)
            {
                LogMessage("Warning: Shields are down.", MessageType.Warning);
            }
        }
        else if (Enterprise.Energy < 300)
        {
            LogMessage("Condition YELLOW: Low energy level.", MessageType.Warning);
        }

        return result;
    }

    public LongRangeScanResult LongRangeScan()
    {
        var result = _scanner.LongRangeScan(Enterprise, Galaxy);

        if (!result.Success)
        {
            LogMessage(result.Message, MessageType.Warning);
        }

        return result;
    }

    public GalacticRecordResult GetGalacticRecord()
    {
        var result = _scanner.GetGalacticRecord(Enterprise, Galaxy);

        if (!result.Success)
        {
            LogMessage(result.Message, MessageType.Warning);
        }

        return result;
    }

    public SystemStatusResult GetSystemStatus()
    {
        return _damage.GetSystemStatus(Enterprise);
    }

    public IReadOnlyList<TorpedoSolution> CalculateTorpedoSolutions()
    {
        if (Enterprise.Damage.IsDamaged(ShipSystem.Computer))
        {
            LogMessage("The main computer is damaged. Repairs are underway.", MessageType.Warning);
            return Array.Empty<TorpedoSolution>();
        }

        var solutions = new List<TorpedoSolution>();

        foreach (var ship in _klingonShips)
        {
            solutions.Add(new TorpedoSolution
            {
                TargetPosition = ship.SectorPosition,
                Direction = NavigationService.ComputeDirection(Enterprise.SectorPosition, ship.SectorPosition),
                Distance = Enterprise.SectorPosition.DistanceTo(ship.SectorPosition) / 8.0
            });
        }

        return solutions;
    }

    public NavigationSolution CalculateNavigationTo(Position targetQuadrant)
    {
        if (Enterprise.Damage.IsDamaged(ShipSystem.Computer))
        {
            LogMessage("The main computer is damaged. Repairs are underway.", MessageType.Warning);
            return new NavigationSolution
            {
                TargetPosition = targetQuadrant,
                Direction = 0,
                Distance = 0
            };
        }

        return new NavigationSolution
        {
            TargetPosition = targetQuadrant,
            Direction = NavigationService.ComputeDirection(Enterprise.QuadrantPosition, targetQuadrant),
            Distance = Enterprise.QuadrantPosition.DistanceTo(targetQuadrant)
        };
    }

    public NavigationSolution? CalculateStarbaseSolution()
    {
        if (Enterprise.Damage.IsDamaged(ShipSystem.Computer))
        {
            LogMessage("The main computer is damaged. Repairs are underway.", MessageType.Warning);
            return null;
        }

        if (!CurrentQuadrant.HasStarbase)
        {
            LogMessage("There are no starbases in this quadrant.", MessageType.Info);
            return null;
        }

        return new NavigationSolution
        {
            TargetPosition = _starbasePosition,
            Direction = NavigationService.ComputeDirection(Enterprise.SectorPosition, _starbasePosition),
            Distance = Enterprise.SectorPosition.DistanceTo(_starbasePosition) / 8.0
        };
    }

    #endregion

    #region Private Methods

    private void GenerateSector()
    {
        var quadrant = CurrentQuadrant;
        _klingonShips.Clear();

        // Clear sector grid
        quadrant.ClearSectors();

        // Place Enterprise
        quadrant[Enterprise.SectorPosition] = SectorContent.Enterprise;

        // Track what needs to be placed
        bool needStarbase = quadrant.HasStarbase;
        int starsRemaining = quadrant.StarCount;
        int klingonsRemaining = quadrant.KlingonCount;

        // Place objects
        while (needStarbase || starsRemaining > 0 || klingonsRemaining > 0)
        {
            int x = _random.Next(8);
            int y = _random.Next(8);
            var pos = new Position(x, y);

            if (IsSectorRegionEmpty(quadrant, pos))
            {
                if (needStarbase)
                {
                    quadrant[pos] = SectorContent.Starbase;
                    _starbasePosition = pos;
                    needStarbase = false;
                }
                else if (starsRemaining > 0)
                {
                    quadrant[pos] = SectorContent.Star;
                    starsRemaining--;
                }
                else if (klingonsRemaining > 0)
                {
                    quadrant[pos] = SectorContent.Klingon;
                    _klingonShips.Add(new KlingonShip
                    {
                        SectorPosition = pos,
                        ShieldLevel = 300 + _random.Next(200)
                    });
                    klingonsRemaining--;
                }
            }
        }
    }

    private bool IsSectorRegionEmpty(Quadrant quadrant, Position pos)
    {
        // Check if position and surrounding area is relatively empty
        for (int dy = -1; dy <= 1; dy++)
        {
            int y = pos.Y + dy;
            if (y < 0 || y > 7) continue;

            if (GetSectorContent(quadrant, pos.X - 1, y) != SectorContent.Empty &&
                GetSectorContent(quadrant, pos.X + 1, y) != SectorContent.Empty)
            {
                return false;
            }
        }

        return GetSectorContent(quadrant, pos.X, pos.Y) == SectorContent.Empty;
    }

    private SectorContent GetSectorContent(Quadrant quadrant, int x, int y)
    {
        if (x < 0 || x > 7 || y < 0 || y > 7)
            return SectorContent.Empty;
        return quadrant[x, y];
    }

    private void LogKlingonAttacks(KlingonAttackResult result)
    {
        foreach (var attack in result.Attacks)
        {
            LogMessage(attack.Message, attack.BlockedByStarbase ? MessageType.Info : MessageType.Damage);
        }
    }

    private void EndGame(GameOutcome outcome)
    {
        State.IsGameActive = false;
        State.IsVictory = outcome == GameOutcome.Victory;

        string message = outcome switch
        {
            GameOutcome.Victory => "MISSION ACCOMPLISHED: ALL KLINGON SHIPS DESTROYED. WELL DONE!!!",
            GameOutcome.Destroyed => "MISSION FAILED: ENTERPRISE DESTROYED!!!",
            GameOutcome.OutOfEnergy => "MISSION FAILED: ENTERPRISE RAN OUT OF ENERGY.",
            GameOutcome.OutOfTime => "MISSION FAILED: ENTERPRISE RAN OUT OF TIME.",
            _ => "GAME OVER"
        };

        LogMessage(message, outcome == GameOutcome.Victory ? MessageType.Success : MessageType.Alert);

        int klingonsDestroyed = State.InitialKlingons - Galaxy.TotalKlingons;
        int score = CalculateScore(outcome, klingonsDestroyed);

        GameOver?.Invoke(this, new GameOverEventArgs(
            message, outcome, score, klingonsDestroyed, Galaxy.TotalStarbases));
    }

    private int CalculateScore(GameOutcome outcome, int klingonsDestroyed)
    {
        if (outcome != GameOutcome.Victory)
            return klingonsDestroyed * 100;

        // Victory score based on efficiency
        int baseScore = klingonsDestroyed * 100;
        int timeBonus = State.TimeRemaining * 10;
        int energyBonus = Enterprise.Energy / 10;

        return baseScore + timeBonus + energyBonus;
    }

    private void CheckTimeLimit()
    {
        if (State.TimeRemaining <= 0)
        {
            EndGame(GameOutcome.OutOfTime);
        }
    }

    private void LogMessage(string message, MessageType type)
    {
        MessageLogged?.Invoke(this, new GameMessageEventArgs(message, type));
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}
