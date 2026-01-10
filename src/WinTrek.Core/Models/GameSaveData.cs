using WinTrek.Core.Enums;

namespace WinTrek.Core.Models;

/// <summary>
/// Represents serializable game save data.
/// </summary>
public class GameSaveData
{
    public int Version { get; set; } = 1;
    public DateTime SavedAt { get; set; }

    // Game state
    public int Stardate { get; set; }
    public int TimeRemaining { get; set; }
    public bool IsGameActive { get; set; }

    // Enterprise state
    public EnterpriseSaveData Enterprise { get; set; } = new();

    // Galaxy state
    public GalaxySaveData Galaxy { get; set; } = new();

    // Current quadrant Klingons
    public List<KlingonSaveData> CurrentKlingons { get; set; } = new();
}

public class EnterpriseSaveData
{
    public int QuadrantX { get; set; }
    public int QuadrantY { get; set; }
    public int SectorX { get; set; }
    public int SectorY { get; set; }
    public int Energy { get; set; }
    public int ShieldLevel { get; set; }
    public int PhotonTorpedoes { get; set; }
    public bool IsDocked { get; set; }
    public Dictionary<string, int> DamageState { get; set; } = new();
}

public class GalaxySaveData
{
    public int TotalKlingons { get; set; }
    public int TotalStarbases { get; set; }
    public List<QuadrantSaveData> Quadrants { get; set; } = new();
}

public class QuadrantSaveData
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Name { get; set; } = string.Empty;
    public int KlingonCount { get; set; }
    public bool HasStarbase { get; set; }
    public int StarCount { get; set; }
    public bool IsScanned { get; set; }
    public int StarbaseX { get; set; }
    public int StarbaseY { get; set; }
    public List<int[]> StarPositions { get; set; } = new();
}

public class KlingonSaveData
{
    public int SectorX { get; set; }
    public int SectorY { get; set; }
    public int ShieldLevel { get; set; }
}
