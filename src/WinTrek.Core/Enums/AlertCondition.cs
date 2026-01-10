namespace WinTrek.Core.Enums;

/// <summary>
/// Represents the current alert status of the Enterprise.
/// </summary>
public enum AlertCondition
{
    /// <summary>Normal operations, no threats detected.</summary>
    Green,

    /// <summary>Low energy warning.</summary>
    Yellow,

    /// <summary>Klingon ships detected in current quadrant.</summary>
    Red
}
