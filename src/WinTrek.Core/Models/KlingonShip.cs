namespace WinTrek.Core.Models;

/// <summary>
/// Represents a Klingon warship within a sector.
/// </summary>
public class KlingonShip
{
    /// <summary>
    /// The ship's position within the current quadrant's sector grid.
    /// </summary>
    public Position SectorPosition { get; set; }

    /// <summary>
    /// The ship's current shield strength. Ship is destroyed when this reaches 0.
    /// </summary>
    public int ShieldLevel { get; set; }

    /// <summary>
    /// Whether this ship has been destroyed.
    /// </summary>
    public bool IsDestroyed => ShieldLevel <= 0;

    /// <summary>
    /// Applies damage to this ship's shields.
    /// </summary>
    /// <param name="damage">Amount of damage to apply.</param>
    /// <returns>True if the ship was destroyed by this damage.</returns>
    public bool TakeDamage(int damage)
    {
        ShieldLevel -= damage;
        if (ShieldLevel < 0)
        {
            ShieldLevel = 0;
        }
        return IsDestroyed;
    }
}
