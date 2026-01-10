namespace WinTrek.Core.Models;

/// <summary>
/// Represents a coordinate position in either a quadrant grid or sector grid.
/// X = column (0-7, left to right), Y = row (0-7, top to bottom).
/// </summary>
public readonly record struct Position(int X, int Y)
{
    /// <summary>
    /// Calculates the Euclidean distance to another position.
    /// </summary>
    public double DistanceTo(Position other)
    {
        double dx = other.X - X;
        double dy = other.Y - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// Checks if this position is within the valid grid bounds.
    /// </summary>
    public bool IsValid(int gridSize = 8) =>
        X >= 0 && X < gridSize && Y >= 0 && Y < gridSize;

    /// <summary>
    /// Returns a display-friendly string with 1-based coordinates.
    /// </summary>
    public string ToDisplayString() => $"[{X + 1},{Y + 1}]";

    public override string ToString() => $"({X},{Y})";
}
