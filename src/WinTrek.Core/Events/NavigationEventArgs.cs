using WinTrek.Core.Models;

namespace WinTrek.Core.Events;

/// <summary>
/// Event fired when navigation occurs.
/// </summary>
public class NavigationEventArgs : GameEventArgs
{
    public Position FromQuadrant { get; }
    public Position ToQuadrant { get; }
    public Position FromSector { get; }
    public Position ToSector { get; }
    public bool QuadrantChanged { get; }
    public int EnergyUsed { get; }

    public NavigationEventArgs(
        string message,
        Position fromQuadrant, Position toQuadrant,
        Position fromSector, Position toSector,
        int energyUsed)
        : base(message)
    {
        FromQuadrant = fromQuadrant;
        ToQuadrant = toQuadrant;
        FromSector = fromSector;
        ToSector = toSector;
        QuadrantChanged = fromQuadrant != toQuadrant;
        EnergyUsed = energyUsed;
    }
}

/// <summary>
/// Event fired when docking at a starbase.
/// </summary>
public class DockingEventArgs : GameEventArgs
{
    public Position StarbasePosition { get; }

    public DockingEventArgs(string message, Position starbasePosition)
        : base(message)
    {
        StarbasePosition = starbasePosition;
    }
}

/// <summary>
/// Event fired when navigation is blocked by an obstacle.
/// </summary>
public class NavigationBlockedEventArgs : GameEventArgs
{
    public Position ObstaclePosition { get; }

    public NavigationBlockedEventArgs(string message, Position obstacle)
        : base(message)
    {
        ObstaclePosition = obstacle;
    }
}
