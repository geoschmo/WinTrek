using WinTrek.Core.Enums;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Handles short-range and long-range scanning operations.
/// </summary>
public class ScannerService
{
    /// <summary>
    /// Performs a short-range scan of the current quadrant.
    /// </summary>
    public ShortRangeScanResult ShortRangeScan(
        Enterprise enterprise,
        Quadrant currentQuadrant)
    {
        // Check for damage
        if (enterprise.Damage.IsDamaged(ShipSystem.ShortRangeScanner))
        {
            return new ShortRangeScanResult
            {
                Success = false,
                Message = "Short range scanner is damaged. Repairs are underway."
            };
        }

        // Mark quadrant as scanned
        currentQuadrant.IsScanned = true;

        // Build sector grid data
        var sectorGrid = new SectorContent[8, 8];
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                sectorGrid[y, x] = currentQuadrant.Sectors[y, x];
            }
        }

        return new ShortRangeScanResult
        {
            Success = true,
            QuadrantName = currentQuadrant.Name,
            QuadrantPosition = enterprise.QuadrantPosition,
            SectorPosition = enterprise.SectorPosition,
            Condition = enterprise.GetCondition(currentQuadrant.KlingonCount),
            SectorGrid = sectorGrid,
            KlingonCount = currentQuadrant.KlingonCount,
            HasStarbase = currentQuadrant.HasStarbase,
            StarCount = currentQuadrant.StarCount
        };
    }

    /// <summary>
    /// Performs a long-range scan of surrounding quadrants.
    /// </summary>
    public LongRangeScanResult LongRangeScan(
        Enterprise enterprise,
        Galaxy galaxy)
    {
        // Check for damage
        if (enterprise.Damage.IsDamaged(ShipSystem.LongRangeScanner))
        {
            return new LongRangeScanResult
            {
                Success = false,
                Message = "Long range scanner is damaged. Repairs are underway."
            };
        }

        var result = new LongRangeScanResult
        {
            Success = true,
            CenterPosition = enterprise.QuadrantPosition
        };

        // Scan 3x3 grid around current position
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                int qx = enterprise.QuadrantPosition.X + dx;
                int qy = enterprise.QuadrantPosition.Y + dy;

                var scanData = new QuadrantScanData
                {
                    Position = new Position(qx, qy),
                    IsValid = Galaxy.IsValidPosition(qx, qy)
                };

                if (scanData.IsValid)
                {
                    var quadrant = galaxy[qx, qy];
                    quadrant.IsScanned = true;

                    scanData.KlingonCount = quadrant.KlingonCount;
                    scanData.HasStarbase = quadrant.HasStarbase;
                    scanData.StarCount = quadrant.StarCount;
                    scanData.Name = quadrant.Name;
                }

                result.ScannedQuadrants.Add(scanData);
            }
        }

        return result;
    }

    /// <summary>
    /// Gets the galactic record (all scanned quadrants).
    /// </summary>
    public GalacticRecordResult GetGalacticRecord(
        Enterprise enterprise,
        Galaxy galaxy)
    {
        // Check for computer damage
        if (enterprise.Damage.IsDamaged(ShipSystem.Computer))
        {
            return new GalacticRecordResult
            {
                Success = false,
                Message = "The main computer is damaged. Repairs are underway."
            };
        }

        var result = new GalacticRecordResult
        {
            Success = true,
            CurrentPosition = enterprise.QuadrantPosition
        };

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                var quadrant = galaxy[x, y];
                result.Quadrants.Add(new QuadrantScanData
                {
                    Position = new Position(x, y),
                    IsValid = true,
                    IsScanned = quadrant.IsScanned,
                    KlingonCount = quadrant.IsScanned ? quadrant.KlingonCount : 0,
                    HasStarbase = quadrant.IsScanned && quadrant.HasStarbase,
                    StarCount = quadrant.IsScanned ? quadrant.StarCount : 0,
                    Name = quadrant.Name
                });
            }
        }

        return result;
    }
}

#region Result Types

public class ShortRangeScanResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string QuadrantName { get; set; } = string.Empty;
    public Position QuadrantPosition { get; set; }
    public Position SectorPosition { get; set; }
    public AlertCondition Condition { get; set; }
    public SectorContent[,]? SectorGrid { get; set; }
    public int KlingonCount { get; set; }
    public bool HasStarbase { get; set; }
    public int StarCount { get; set; }
}

public class LongRangeScanResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Position CenterPosition { get; set; }
    public List<QuadrantScanData> ScannedQuadrants { get; } = new();
}

public class GalacticRecordResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Position CurrentPosition { get; set; }
    public List<QuadrantScanData> Quadrants { get; } = new();
}

public class QuadrantScanData
{
    public Position Position { get; set; }
    public bool IsValid { get; set; }
    public bool IsScanned { get; set; } = true;
    public string Name { get; set; } = string.Empty;
    public int KlingonCount { get; set; }
    public bool HasStarbase { get; set; }
    public int StarCount { get; set; }

    /// <summary>
    /// Gets the three-digit scan code (KSS format).
    /// </summary>
    public string ScanCode => $"{KlingonCount}{(HasStarbase ? 1 : 0)}{StarCount}";
}

#endregion
