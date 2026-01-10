namespace WinTrek.Core.Models;

/// <summary>
/// Represents the 8x8 galactic grid containing all quadrants.
/// </summary>
public class Galaxy
{
    public const int Size = 8;

    private readonly Quadrant[,] _quadrants = new Quadrant[Size, Size];

    /// <summary>
    /// Total number of Klingon ships remaining in the galaxy.
    /// </summary>
    public int TotalKlingons { get; private set; }

    /// <summary>
    /// Total number of starbases in the galaxy.
    /// </summary>
    public int TotalStarbases { get; private set; }

    /// <summary>
    /// Gets a quadrant by position.
    /// </summary>
    public Quadrant this[Position pos] => _quadrants[pos.Y, pos.X];

    /// <summary>
    /// Gets a quadrant by coordinates.
    /// </summary>
    public Quadrant this[int x, int y] => _quadrants[y, x];

    /// <summary>
    /// Initializes the galaxy with random quadrant contents.
    /// </summary>
    /// <param name="random">Random number generator.</param>
    /// <param name="quadrantNames">Array of 64+ unique quadrant names.</param>
    /// <param name="totalKlingons">Total number of Klingons to place (15-20 typically).</param>
    /// <param name="totalStarbases">Total number of starbases to place (2-4 typically).</param>
    public void Initialize(Random random, string[] quadrantNames, int totalKlingons, int totalStarbases)
    {
        TotalKlingons = totalKlingons;
        TotalStarbases = totalStarbases;

        // Shuffle names for random assignment
        var names = new List<string>(quadrantNames);

        // Create all quadrants with names and star counts
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                int nameIndex = random.Next(names.Count);
                _quadrants[y, x] = new Quadrant
                {
                    Name = names[nameIndex],
                    StarCount = 1 + random.Next(8)
                };
                names.RemoveAt(nameIndex);
            }
        }

        // Distribute Klingons and starbases
        int klingonsRemaining = totalKlingons;
        int starbasesRemaining = totalStarbases;

        while (klingonsRemaining > 0 || starbasesRemaining > 0)
        {
            int x = random.Next(Size);
            int y = random.Next(Size);
            var quadrant = _quadrants[y, x];

            if (starbasesRemaining > 0 && !quadrant.HasStarbase)
            {
                quadrant.HasStarbase = true;
                starbasesRemaining--;
            }

            if (klingonsRemaining > 0 && quadrant.KlingonCount < 3)
            {
                quadrant.KlingonCount++;
                klingonsRemaining--;
            }
        }
    }

    /// <summary>
    /// Decrements the total Klingon count when one is destroyed.
    /// </summary>
    public void KlingonDestroyed()
    {
        if (TotalKlingons > 0)
            TotalKlingons--;
    }

    /// <summary>
    /// Decrements the total starbase count when one is destroyed.
    /// </summary>
    public void StarbaseDestroyed()
    {
        if (TotalStarbases > 0)
            TotalStarbases--;
    }

    /// <summary>
    /// Checks if a position is within the galaxy bounds.
    /// </summary>
    public static bool IsValidPosition(Position pos) =>
        pos.X >= 0 && pos.X < Size && pos.Y >= 0 && pos.Y < Size;

    /// <summary>
    /// Checks if a position is within the galaxy bounds.
    /// </summary>
    public static bool IsValidPosition(int x, int y) =>
        x >= 0 && x < Size && y >= 0 && y < Size;
}
