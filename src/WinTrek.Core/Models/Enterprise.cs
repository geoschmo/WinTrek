using WinTrek.Core.Enums;

namespace WinTrek.Core.Models;

/// <summary>
/// Represents the USS Enterprise, the player's starship.
/// </summary>
public class Enterprise
{
    public const int MaxEnergy = 3000;
    public const int MaxTorpedoes = 10;

    /// <summary>
    /// The Enterprise's position within the 8x8 galactic grid.
    /// </summary>
    public Position QuadrantPosition { get; set; }

    /// <summary>
    /// The Enterprise's position within the current quadrant's 8x8 sector grid.
    /// </summary>
    public Position SectorPosition { get; set; }

    /// <summary>
    /// Current energy level. Used for movement and phasers.
    /// </summary>
    public int Energy { get; set; } = MaxEnergy;

    /// <summary>
    /// Current shield strength. Absorbs damage from Klingon attacks.
    /// </summary>
    public int ShieldLevel { get; set; }

    /// <summary>
    /// Number of photon torpedoes remaining.
    /// </summary>
    public int PhotonTorpedoes { get; set; } = MaxTorpedoes;

    /// <summary>
    /// Whether the Enterprise is currently docked at a starbase.
    /// </summary>
    public bool IsDocked { get; set; }

    /// <summary>
    /// Whether the Enterprise has been destroyed.
    /// </summary>
    public bool IsDestroyed { get; set; }

    /// <summary>
    /// Tracks damage to all ship systems.
    /// </summary>
    public DamageState Damage { get; } = new();

    /// <summary>
    /// Gets the current alert condition based on game state.
    /// </summary>
    public AlertCondition GetCondition(int klingonsInQuadrant)
    {
        if (klingonsInQuadrant > 0)
            return AlertCondition.Red;
        if (Energy < 300)
            return AlertCondition.Yellow;
        return AlertCondition.Green;
    }

    /// <summary>
    /// Transfers energy to or from shields.
    /// </summary>
    /// <param name="amount">Positive to add to shields, negative to remove.</param>
    /// <returns>True if transfer was successful.</returns>
    public bool TransferShields(int amount)
    {
        if (amount > 0)
        {
            // Adding to shields
            if (amount > Energy)
                return false;
            Energy -= amount;
            ShieldLevel += amount;
        }
        else
        {
            // Removing from shields
            int removeAmount = Math.Abs(amount);
            if (removeAmount > ShieldLevel)
                return false;
            ShieldLevel -= removeAmount;
            Energy += removeAmount;
        }
        return true;
    }

    /// <summary>
    /// Applies damage to the Enterprise from a Klingon attack.
    /// </summary>
    /// <param name="damage">Amount of damage to apply.</param>
    /// <returns>True if the Enterprise was destroyed.</returns>
    public bool TakeDamage(int damage)
    {
        ShieldLevel -= damage;
        if (ShieldLevel < 0)
        {
            ShieldLevel = 0;
            IsDestroyed = true;
        }
        return IsDestroyed;
    }

    /// <summary>
    /// Docks at a starbase, restoring all resources and repairing all damage.
    /// </summary>
    public void Dock()
    {
        IsDocked = true;
        Energy = MaxEnergy;
        PhotonTorpedoes = MaxTorpedoes;
        ShieldLevel = 0; // Shields lowered for docking
        Damage.RepairAll();
    }

    /// <summary>
    /// Undocks from a starbase.
    /// </summary>
    public void Undock()
    {
        IsDocked = false;
    }

    /// <summary>
    /// Resets the Enterprise to initial state for a new game.
    /// </summary>
    public void Reset()
    {
        Energy = MaxEnergy;
        ShieldLevel = 0;
        PhotonTorpedoes = MaxTorpedoes;
        IsDocked = false;
        IsDestroyed = false;
        Damage.RepairAll();
    }
}
