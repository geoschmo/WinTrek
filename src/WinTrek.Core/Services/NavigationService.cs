using WinTrek.Core.Enums;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Handles navigation and movement calculations.
/// </summary>
public class NavigationService
{
    private readonly Random _random;

    public NavigationService(Random random)
    {
        _random = random;
    }

    /// <summary>
    /// Computes the direction (1-9) from one position to another.
    /// Direction 1 = East, 3 = South, 5 = West, 7 = North, with diagonals between.
    /// </summary>
    public static double ComputeDirection(Position from, Position to)
    {
        int x1 = from.X, y1 = from.Y;
        int x2 = to.X, y2 = to.Y;

        if (x1 == x2)
        {
            return y1 < y2 ? 7.0 : 3.0; // North or South
        }

        if (y1 == y2)
        {
            return x1 < x2 ? 1.0 : 5.0; // East or West
        }

        double dy = Math.Abs(y2 - y1);
        double dx = Math.Abs(x2 - x1);
        double angle = Math.Atan2(dy, dx);

        if (x1 < x2)
        {
            return y1 < y2
                ? 9.0 - 4.0 * angle / Math.PI   // NE quadrant
                : 1.0 + 4.0 * angle / Math.PI;  // SE quadrant
        }
        else
        {
            return y1 < y2
                ? 5.0 + 4.0 * angle / Math.PI   // NW quadrant
                : 5.0 - 4.0 * angle / Math.PI;  // SW quadrant
        }
    }

    /// <summary>
    /// Computes the distance between two positions.
    /// </summary>
    public static double ComputeDistance(Position from, Position to)
    {
        return from.DistanceTo(to);
    }

    /// <summary>
    /// Gets the maximum warp factor based on engine damage.
    /// </summary>
    public double GetMaxWarpFactor(int navigationDamage)
    {
        if (navigationDamage > 0)
        {
            return 0.2 + _random.Next(9) / 10.0;
        }
        return 8.0;
    }

    /// <summary>
    /// Calculates energy required for a given warp factor.
    /// </summary>
    public static int CalculateEnergyRequired(double warpFactor)
    {
        return (int)(warpFactor * 8);
    }

    /// <summary>
    /// Attempts to navigate the Enterprise.
    /// </summary>
    /// <param name="enterprise">The Enterprise.</param>
    /// <param name="galaxy">The galaxy.</param>
    /// <param name="currentQuadrant">The current quadrant (will be modified if movement within quadrant).</param>
    /// <param name="direction">Course direction (1.0 - 9.0).</param>
    /// <param name="warpFactor">Warp speed (0.1 - 8.0).</param>
    /// <returns>Navigation result with details of what happened.</returns>
    public NavigationResult Navigate(
        Enterprise enterprise,
        Galaxy galaxy,
        Quadrant currentQuadrant,
        double direction,
        double warpFactor)
    {
        // Validate direction
        if (direction < 1.0 || direction > 9.0)
        {
            return NavigationResult.Failed("Invalid course.");
        }

        // Check for damaged engines
        double maxWarp = GetMaxWarpFactor(enterprise.Damage[ShipSystem.WarpEngines]);
        if (warpFactor < 0.1 || warpFactor > maxWarp)
        {
            if (enterprise.Damage[ShipSystem.WarpEngines] > 0)
            {
                return NavigationResult.Failed($"Warp engines damaged. Maximum warp factor: {maxWarp:F1}");
            }
            return NavigationResult.Failed("Invalid warp factor.");
        }

        // Check energy
        int energyRequired = CalculateEnergyRequired(warpFactor);
        if (energyRequired >= enterprise.Energy)
        {
            return NavigationResult.Failed("Unable to comply. Insufficient energy to travel that speed.");
        }

        // Store starting position
        var startQuadrant = enterprise.QuadrantPosition;
        var startSector = enterprise.SectorPosition;

        // Consume energy
        enterprise.Energy -= energyRequired;

        // Calculate movement
        double distance = warpFactor * 8;
        double angle = -(Math.PI * (direction - 1.0) / 4.0);

        // Convert to absolute galactic coordinates (0-63)
        double x = enterprise.QuadrantPosition.X * 8 + enterprise.SectorPosition.X;
        double y = enterprise.QuadrantPosition.Y * 8 + enterprise.SectorPosition.Y;

        double dx = distance * Math.Cos(angle);
        double dy = distance * Math.Sin(angle);
        double vx = dx / 1000;
        double vy = dy / 1000;

        // Clear current position
        currentQuadrant[enterprise.SectorPosition] = SectorContent.Empty;

        int lastSectX = enterprise.SectorPosition.X;
        int lastSectY = enterprise.SectorPosition.Y;
        bool hitObstacle = false;

        // Simulate movement in small steps
        for (int i = 0; i < 1000; i++)
        {
            x += vx;
            y += vy;

            int quadX = (int)Math.Round(x) / 8;
            int quadY = (int)Math.Round(y) / 8;

            // Check for obstacles within current quadrant
            if (quadX == enterprise.QuadrantPosition.X && quadY == enterprise.QuadrantPosition.Y)
            {
                int sectX = (int)Math.Round(x) % 8;
                int sectY = (int)Math.Round(y) % 8;

                if (sectX >= 0 && sectX < 8 && sectY >= 0 && sectY < 8)
                {
                    if (currentQuadrant[sectX, sectY] != SectorContent.Empty)
                    {
                        // Hit obstacle - stop at last valid position
                        enterprise.SectorPosition = new Position(lastSectX, lastSectY);
                        currentQuadrant[enterprise.SectorPosition] = SectorContent.Enterprise;
                        hitObstacle = true;
                        break;
                    }
                    lastSectX = sectX;
                    lastSectY = sectY;
                }
            }
        }

        if (hitObstacle)
        {
            return new NavigationResult
            {
                Success = true,
                HitObstacle = true,
                Message = "Encountered obstacle within quadrant.",
                StartQuadrant = startQuadrant,
                EndQuadrant = enterprise.QuadrantPosition,
                StartSector = startSector,
                EndSector = enterprise.SectorPosition,
                EnergyUsed = energyRequired,
                QuadrantChanged = false
            };
        }

        // Clamp to galaxy bounds
        x = Math.Clamp(x, 0, 63);
        y = Math.Clamp(y, 0, 63);

        // Calculate final position
        int finalQuadX = (int)Math.Round(x) / 8;
        int finalQuadY = (int)Math.Round(y) / 8;
        int finalSectX = (int)Math.Round(x) % 8;
        int finalSectY = (int)Math.Round(y) % 8;

        // Clamp to valid ranges
        finalQuadX = Math.Clamp(finalQuadX, 0, 7);
        finalQuadY = Math.Clamp(finalQuadY, 0, 7);
        finalSectX = Math.Clamp(finalSectX, 0, 7);
        finalSectY = Math.Clamp(finalSectY, 0, 7);

        bool quadrantChanged = finalQuadX != enterprise.QuadrantPosition.X ||
                               finalQuadY != enterprise.QuadrantPosition.Y;

        enterprise.QuadrantPosition = new Position(finalQuadX, finalQuadY);
        enterprise.SectorPosition = new Position(finalSectX, finalSectY);

        if (!quadrantChanged)
        {
            // Place Enterprise in new sector position
            currentQuadrant[enterprise.SectorPosition] = SectorContent.Enterprise;
        }

        return new NavigationResult
        {
            Success = true,
            Message = "Warp engines engaged.",
            StartQuadrant = startQuadrant,
            EndQuadrant = enterprise.QuadrantPosition,
            StartSector = startSector,
            EndSector = enterprise.SectorPosition,
            EnergyUsed = energyRequired,
            QuadrantChanged = quadrantChanged
        };
    }

    /// <summary>
    /// Checks if a sector position is adjacent to a starbase (for docking).
    /// </summary>
    public static bool IsDockingLocation(Quadrant quadrant, Position sectorPosition)
    {
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                int x = sectorPosition.X + dx;
                int y = sectorPosition.Y + dy;

                if (x >= 0 && x < 8 && y >= 0 && y < 8)
                {
                    if (quadrant[x, y] == SectorContent.Starbase)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}

/// <summary>
/// Result of a navigation attempt.
/// </summary>
public class NavigationResult
{
    public bool Success { get; init; }
    public bool HitObstacle { get; init; }
    public string Message { get; init; } = string.Empty;
    public Position StartQuadrant { get; init; }
    public Position EndQuadrant { get; init; }
    public Position StartSector { get; init; }
    public Position EndSector { get; init; }
    public int EnergyUsed { get; init; }
    public bool QuadrantChanged { get; init; }

    public static NavigationResult Failed(string message) => new()
    {
        Success = false,
        Message = message
    };
}
