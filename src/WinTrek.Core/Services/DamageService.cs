using WinTrek.Core.Enums;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Handles damage induction and repair for ship systems.
/// </summary>
public class DamageService
{
    private readonly Random _random;

    public DamageService(Random random)
    {
        _random = random;
    }

    /// <summary>
    /// Attempts to induce random damage to a ship system.
    /// Called after navigation when not docked and no Klingons present.
    /// </summary>
    public DamageResult TryInduceDamage(Enterprise enterprise)
    {
        var damagedSystem = enterprise.Damage.InduceRandom(_random);

        if (damagedSystem == null)
        {
            return new DamageResult { DamageOccurred = false };
        }

        return new DamageResult
        {
            DamageOccurred = true,
            AffectedSystem = damagedSystem.Value,
            Message = GetDamageMessage(damagedSystem.Value)
        };
    }

    /// <summary>
    /// Induces damage to a specific system (e.g., from using the computer).
    /// </summary>
    public DamageResult TryInduceDamageToSystem(Enterprise enterprise, ShipSystem system)
    {
        // 1 in 7 chance
        if (_random.Next(7) > 0)
        {
            return new DamageResult { DamageOccurred = false };
        }

        int damageAmount = 1 + _random.Next(5);
        enterprise.Damage.InduceDamage(system, damageAmount);

        return new DamageResult
        {
            DamageOccurred = true,
            AffectedSystem = system,
            DamageAmount = damageAmount,
            Message = GetDamageMessage(system)
        };
    }

    /// <summary>
    /// Repairs one unit of damage from the first damaged system.
    /// Called after navigation when not in combat.
    /// </summary>
    public RepairResult TryRepairDamage(Enterprise enterprise)
    {
        var (system, fullyRepaired) = enterprise.Damage.RepairOne();

        if (system == null)
        {
            return new RepairResult { RepairOccurred = false };
        }

        return new RepairResult
        {
            RepairOccurred = true,
            AffectedSystem = system.Value,
            FullyRepaired = fullyRepaired,
            Message = fullyRepaired ? GetRepairCompleteMessage(system.Value) : null
        };
    }

    /// <summary>
    /// Gets the status of all ship systems.
    /// </summary>
    public SystemStatusResult GetSystemStatus(Enterprise enterprise)
    {
        var result = new SystemStatusResult();

        foreach (ShipSystem system in Enum.GetValues<ShipSystem>())
        {
            result.Systems.Add(new SystemStatus
            {
                System = system,
                Name = DamageState.GetSystemName(system),
                DamageLevel = enterprise.Damage[system],
                IsOperational = !enterprise.Damage.IsDamaged(system)
            });
        }

        return result;
    }

    private static string GetDamageMessage(ShipSystem system) => system switch
    {
        ShipSystem.WarpEngines => "Warp engines are malfunctioning.",
        ShipSystem.ShortRangeScanner => "Short range scanner is malfunctioning.",
        ShipSystem.LongRangeScanner => "Long range scanner is malfunctioning.",
        ShipSystem.ShieldControl => "Shield controls are malfunctioning.",
        ShipSystem.Computer => "The main computer is malfunctioning.",
        ShipSystem.PhotonTorpedo => "Photon torpedo controls are malfunctioning.",
        ShipSystem.Phaser => "Phasers are malfunctioning.",
        _ => $"{system} is malfunctioning."
    };

    private static string GetRepairCompleteMessage(ShipSystem system) => system switch
    {
        ShipSystem.WarpEngines => "Warp engines have been repaired.",
        ShipSystem.ShortRangeScanner => "Short range scanner has been repaired.",
        ShipSystem.LongRangeScanner => "Long range scanner has been repaired.",
        ShipSystem.ShieldControl => "Shield controls have been repaired.",
        ShipSystem.Computer => "The main computer has been repaired.",
        ShipSystem.PhotonTorpedo => "Photon torpedo controls have been repaired.",
        ShipSystem.Phaser => "Phasers have been repaired.",
        _ => $"{system} has been repaired."
    };
}

#region Result Types

public class DamageResult
{
    public bool DamageOccurred { get; set; }
    public ShipSystem AffectedSystem { get; set; }
    public int DamageAmount { get; set; }
    public string? Message { get; set; }
}

public class RepairResult
{
    public bool RepairOccurred { get; set; }
    public ShipSystem AffectedSystem { get; set; }
    public bool FullyRepaired { get; set; }
    public string? Message { get; set; }
}

public class SystemStatusResult
{
    public List<SystemStatus> Systems { get; } = new();
    public bool HasDamage => Systems.Any(s => !s.IsOperational);
}

public class SystemStatus
{
    public ShipSystem System { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DamageLevel { get; set; }
    public bool IsOperational { get; set; }
}

#endregion
