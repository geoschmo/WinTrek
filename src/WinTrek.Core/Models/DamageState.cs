using WinTrek.Core.Enums;

namespace WinTrek.Core.Models;

/// <summary>
/// Tracks damage levels for all Enterprise ship systems.
/// A damage level > 0 indicates the system is malfunctioning.
/// </summary>
public class DamageState
{
    private readonly Dictionary<ShipSystem, int> _damage = new()
    {
        { ShipSystem.WarpEngines, 0 },
        { ShipSystem.ShortRangeScanner, 0 },
        { ShipSystem.LongRangeScanner, 0 },
        { ShipSystem.ShieldControl, 0 },
        { ShipSystem.Computer, 0 },
        { ShipSystem.PhotonTorpedo, 0 },
        { ShipSystem.Phaser, 0 }
    };

    /// <summary>
    /// Gets or sets the damage level for a specific system.
    /// </summary>
    public int this[ShipSystem system]
    {
        get => _damage[system];
        set => _damage[system] = Math.Max(0, value);
    }

    /// <summary>
    /// Checks if a specific system is damaged (has damage > 0).
    /// </summary>
    public bool IsDamaged(ShipSystem system) => _damage[system] > 0;

    /// <summary>
    /// Gets the damage level for a specific system.
    /// </summary>
    public int GetDamageLevel(ShipSystem system) => _damage[system];

    /// <summary>
    /// Sets the damage level for a specific system.
    /// </summary>
    public void SetDamageLevel(ShipSystem system, int level) => _damage[system] = Math.Max(0, level);

    /// <summary>
    /// Checks if any system is currently damaged.
    /// </summary>
    public bool HasAnyDamage => _damage.Values.Any(d => d > 0);

    /// <summary>
    /// Gets the first damaged system, or null if none are damaged.
    /// </summary>
    public ShipSystem? FirstDamagedSystem =>
        _damage.FirstOrDefault(kvp => kvp.Value > 0).Key is var system && _damage[system] > 0
            ? system
            : null;

    /// <summary>
    /// Repairs one unit of damage from the first damaged system.
    /// </summary>
    /// <returns>The system that was repaired, and whether it's now fully repaired.</returns>
    public (ShipSystem? System, bool FullyRepaired) RepairOne()
    {
        foreach (var system in _damage.Keys)
        {
            if (_damage[system] > 0)
            {
                _damage[system]--;
                return (system, _damage[system] == 0);
            }
        }
        return (null, false);
    }

    /// <summary>
    /// Induces random damage to a random system.
    /// </summary>
    /// <param name="random">Random number generator.</param>
    /// <returns>The system that was damaged, or null if no damage occurred.</returns>
    public ShipSystem? InduceRandom(Random random)
    {
        // 1 in 7 chance of damage occurring
        if (random.Next(7) > 0)
        {
            return null;
        }

        int damageAmount = 1 + random.Next(5);
        var systems = Enum.GetValues<ShipSystem>();
        var system = systems[random.Next(systems.Length)];

        _damage[system] = damageAmount;
        return system;
    }

    /// <summary>
    /// Induces damage to a specific system.
    /// </summary>
    /// <param name="system">The system to damage.</param>
    /// <param name="amount">Amount of damage (1-5 typically).</param>
    public void InduceDamage(ShipSystem system, int amount)
    {
        _damage[system] = amount;
    }

    /// <summary>
    /// Repairs all damage (used when docking at starbase).
    /// </summary>
    public void RepairAll()
    {
        foreach (var system in _damage.Keys)
        {
            _damage[system] = 0;
        }
    }

    /// <summary>
    /// Gets the display name for a ship system.
    /// </summary>
    public static string GetSystemName(ShipSystem system) => system switch
    {
        ShipSystem.WarpEngines => "Warp Engines",
        ShipSystem.ShortRangeScanner => "Short Range Scanner",
        ShipSystem.LongRangeScanner => "Long Range Scanner",
        ShipSystem.ShieldControl => "Shield Controls",
        ShipSystem.Computer => "Main Computer",
        ShipSystem.PhotonTorpedo => "Photon Torpedo Controls",
        ShipSystem.Phaser => "Phasers",
        _ => system.ToString()
    };
}
