using WinTrek.Core.Enums;

namespace WinTrek.Core.Models;

/// <summary>
/// Represents a quadrant in the 8x8 galactic grid.
/// Each quadrant contains an 8x8 sector grid when active.
/// </summary>
public class Quadrant
{
    public const int SectorSize = 8;

    /// <summary>
    /// The Star Trek-themed name of this quadrant (e.g., "Vulcan", "Romulus").
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Number of Klingon ships in this quadrant.
    /// </summary>
    public int KlingonCount { get; set; }

    /// <summary>
    /// Number of stars in this quadrant.
    /// </summary>
    public int StarCount { get; init; }

    /// <summary>
    /// Whether this quadrant contains a starbase.
    /// </summary>
    public bool HasStarbase { get; set; }

    /// <summary>
    /// Whether this quadrant has been scanned by long-range sensors.
    /// </summary>
    public bool IsScanned { get; set; }

    /// <summary>
    /// The 8x8 sector grid for this quadrant. Only populated when the Enterprise is present.
    /// </summary>
    public SectorContent[,] Sectors { get; } = new SectorContent[SectorSize, SectorSize];

    /// <summary>
    /// Returns the three-digit scan code (Klingons, Starbases, Stars).
    /// </summary>
    public string GetScanCode()
    {
        int starbaseCount = HasStarbase ? 1 : 0;
        return $"{KlingonCount}{starbaseCount}{StarCount}";
    }

    /// <summary>
    /// Gets or sets the content at a specific sector position.
    /// </summary>
    public SectorContent this[Position pos]
    {
        get => Sectors[pos.Y, pos.X];
        set => Sectors[pos.Y, pos.X] = value;
    }

    /// <summary>
    /// Gets or sets the content at specific sector coordinates.
    /// </summary>
    public SectorContent this[int x, int y]
    {
        get => Sectors[y, x];
        set => Sectors[y, x] = value;
    }

    /// <summary>
    /// Clears all sectors to empty.
    /// </summary>
    public void ClearSectors()
    {
        for (int y = 0; y < SectorSize; y++)
        {
            for (int x = 0; x < SectorSize; x++)
            {
                Sectors[y, x] = SectorContent.Empty;
            }
        }
    }
}
