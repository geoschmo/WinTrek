namespace WinTrek.Core.Events;

/// <summary>
/// Base class for all game events.
/// </summary>
public class GameEventArgs : EventArgs
{
    public string Message { get; }
    public DateTime Timestamp { get; } = DateTime.Now;

    public GameEventArgs(string message)
    {
        Message = message;
    }
}

/// <summary>
/// Event for general game messages (replaces Console.WriteLine).
/// </summary>
public class GameMessageEventArgs : GameEventArgs
{
    public MessageType Type { get; }

    public GameMessageEventArgs(string message, MessageType type = MessageType.Info)
        : base(message)
    {
        Type = type;
    }
}

/// <summary>
/// Types of game messages for UI styling.
/// </summary>
public enum MessageType
{
    Info,
    Warning,
    Alert,
    Success,
    Damage,
    System
}
