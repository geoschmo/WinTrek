using WinTrek.Core.Models;

namespace WinTrek.Core.Events;

/// <summary>
/// Event fired when combat occurs.
/// </summary>
public class CombatEventArgs : GameEventArgs
{
    public CombatType CombatType { get; }
    public Position TargetPosition { get; }
    public int DamageDealt { get; }
    public bool TargetDestroyed { get; }

    public CombatEventArgs(string message, CombatType type, Position target, int damage, bool destroyed)
        : base(message)
    {
        CombatType = type;
        TargetPosition = target;
        DamageDealt = damage;
        TargetDestroyed = destroyed;
    }
}

/// <summary>
/// Types of combat actions.
/// </summary>
public enum CombatType
{
    PhaserFire,
    TorpedoFire,
    TorpedoHit,
    TorpedoMiss,
    KlingonAttack,
    ShipDestroyed,
    StarbaseDestroyed
}

/// <summary>
/// Event fired when the Enterprise takes damage.
/// </summary>
public class DamageReceivedEventArgs : GameEventArgs
{
    public Position AttackerPosition { get; }
    public int DamageAmount { get; }
    public int RemainingShields { get; }
    public bool ShieldsDown { get; }

    public DamageReceivedEventArgs(string message, Position attacker, int damage, int shields)
        : base(message)
    {
        AttackerPosition = attacker;
        DamageAmount = damage;
        RemainingShields = shields;
        ShieldsDown = shields <= 0;
    }
}
