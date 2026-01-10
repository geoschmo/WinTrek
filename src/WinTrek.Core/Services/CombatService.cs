using WinTrek.Core.Enums;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Handles all combat operations including phasers, torpedoes, and Klingon attacks.
/// </summary>
public class CombatService
{
    private readonly Random _random;

    /// <summary>
    /// Distance divisor used in damage calculations.
    /// </summary>
    private const double DamageDistanceFactor = 11.3;

    /// <summary>
    /// Maximum damage a Klingon can deal per attack.
    /// </summary>
    private const int MaxKlingonDamage = 300;

    public CombatService(Random random)
    {
        _random = random;
    }

    /// <summary>
    /// Fires phasers at all Klingon ships in the quadrant.
    /// </summary>
    public PhaserResult FirePhasers(
        Enterprise enterprise,
        Quadrant currentQuadrant,
        List<KlingonShip> klingonShips,
        int energyToFire)
    {
        var result = new PhaserResult();

        // Check for damage
        if (enterprise.Damage.IsDamaged(ShipSystem.Phaser))
        {
            return PhaserResult.Failed("Phasers are damaged. Repairs are underway.");
        }

        // Check for targets
        if (klingonShips.Count == 0)
        {
            return PhaserResult.Failed("There are no Klingon ships in this quadrant.");
        }

        // Validate energy
        if (energyToFire < 1 || energyToFire > enterprise.Energy)
        {
            return PhaserResult.Failed("Invalid energy level.");
        }

        result.Success = true;
        result.EnergyUsed = 0;
        var destroyedShips = new List<KlingonShip>();

        foreach (var ship in klingonShips)
        {
            // Deduct energy for each shot
            if (enterprise.Energy < energyToFire)
            {
                break;
            }
            enterprise.Energy -= energyToFire;
            result.EnergyUsed += energyToFire;

            // Calculate damage based on distance
            double distance = enterprise.SectorPosition.DistanceTo(ship.SectorPosition);
            int deliveredDamage = (int)(energyToFire * (1.0 - distance / DamageDistanceFactor));

            var hitResult = new PhaserHitResult
            {
                TargetPosition = ship.SectorPosition,
                DamageDealt = deliveredDamage,
                RemainingShields = ship.ShieldLevel - deliveredDamage
            };

            if (ship.TakeDamage(deliveredDamage))
            {
                hitResult.Destroyed = true;
                hitResult.RemainingShields = 0;
                destroyedShips.Add(ship);
            }

            result.Hits.Add(hitResult);
        }

        // Remove destroyed ships
        foreach (var ship in destroyedShips)
        {
            currentQuadrant[ship.SectorPosition] = SectorContent.Empty;
            currentQuadrant.KlingonCount--;
            klingonShips.Remove(ship);
        }

        result.ShipsDestroyed = destroyedShips.Count;
        return result;
    }

    /// <summary>
    /// Fires a photon torpedo in the specified direction.
    /// </summary>
    public TorpedoResult FireTorpedo(
        Enterprise enterprise,
        Quadrant currentQuadrant,
        List<KlingonShip> klingonShips,
        double direction)
    {
        // Check for damage
        if (enterprise.Damage.IsDamaged(ShipSystem.PhotonTorpedo))
        {
            return TorpedoResult.Failed("Photon torpedo control is damaged. Repairs are underway.");
        }

        // Check ammo
        if (enterprise.PhotonTorpedoes == 0)
        {
            return TorpedoResult.Failed("Photon torpedoes exhausted.");
        }

        // Check for targets
        if (klingonShips.Count == 0)
        {
            return TorpedoResult.Failed("There are no Klingon ships in this quadrant.");
        }

        // Validate direction
        if (direction < 1.0 || direction > 9.0)
        {
            return TorpedoResult.Failed("Invalid direction.");
        }

        // Consume torpedo
        enterprise.PhotonTorpedoes--;

        var result = new TorpedoResult { Success = true };

        // Calculate trajectory with possible deviation
        double angle = -(Math.PI * (direction - 1.0) / 4.0);
        if (_random.Next(3) == 0)
        {
            // 1 in 3 chance of slight course deviation
            angle += ((1.0 - 2.0 * _random.NextDouble()) * Math.PI * 2.0) * 0.03;
        }

        double x = enterprise.SectorPosition.X;
        double y = enterprise.SectorPosition.Y;
        double vx = Math.Cos(angle) / 20;
        double vy = Math.Sin(angle) / 20;

        int lastX = -1, lastY = -1;

        // Trace torpedo path
        while (x >= 0 && y >= 0 && Math.Round(x) < 8 && Math.Round(y) < 8)
        {
            int newX = (int)Math.Round(x);
            int newY = (int)Math.Round(y);

            if (lastX != newX || lastY != newY)
            {
                var pos = new Position(newX, newY);
                result.Path.Add(pos);
                lastX = newX;
                lastY = newY;

                // Check for Klingon hit
                foreach (var ship in klingonShips)
                {
                    if (ship.SectorPosition.X == newX && ship.SectorPosition.Y == newY)
                    {
                        result.HitType = TorpedoHitType.Klingon;
                        result.HitPosition = pos;
                        result.Message = $"Klingon ship destroyed at sector {pos.ToDisplayString()}.";

                        currentQuadrant[pos] = SectorContent.Empty;
                        currentQuadrant.KlingonCount--;
                        klingonShips.Remove(ship);
                        return result;
                    }
                }

                // Check for other hits
                switch (currentQuadrant[newX, newY])
                {
                    case SectorContent.Starbase:
                        result.HitType = TorpedoHitType.Starbase;
                        result.HitPosition = pos;
                        result.Message = $"The Enterprise destroyed a Federation starbase at sector {pos.ToDisplayString()}!";
                        currentQuadrant[pos] = SectorContent.Empty;
                        currentQuadrant.HasStarbase = false;
                        return result;

                    case SectorContent.Star:
                        result.HitType = TorpedoHitType.Star;
                        result.HitPosition = pos;
                        result.Message = $"The torpedo was captured by a star's gravitational field at sector {pos.ToDisplayString()}.";
                        return result;
                }
            }

            x += vx;
            y += vy;
        }

        // Missed everything
        result.HitType = TorpedoHitType.Miss;
        result.Message = "Photon torpedo failed to hit anything.";
        return result;
    }

    /// <summary>
    /// Executes Klingon attacks against the Enterprise.
    /// </summary>
    public KlingonAttackResult KlingonsAttack(
        Enterprise enterprise,
        List<KlingonShip> klingonShips)
    {
        var result = new KlingonAttackResult();

        if (klingonShips.Count == 0)
        {
            return result;
        }

        foreach (var ship in klingonShips)
        {
            var attack = new KlingonAttack { AttackerPosition = ship.SectorPosition };

            if (enterprise.IsDocked)
            {
                // Protected by starbase shields
                attack.DamageDealt = 0;
                attack.BlockedByStarbase = true;
                attack.Message = $"Enterprise hit by ship at sector {ship.SectorPosition.ToDisplayString()}. No damage due to starbase shields.";
            }
            else
            {
                double distance = enterprise.SectorPosition.DistanceTo(ship.SectorPosition);
                int damage = (int)(MaxKlingonDamage * _random.NextDouble() * (1.0 - distance / DamageDistanceFactor));

                attack.DamageDealt = damage;
                enterprise.TakeDamage(damage);
                attack.RemainingShields = enterprise.ShieldLevel;
                attack.Message = $"Enterprise hit by ship at sector {ship.SectorPosition.ToDisplayString()}. Shields dropped to {enterprise.ShieldLevel}.";

                if (enterprise.IsDestroyed)
                {
                    result.EnterpriseDestroyed = true;
                }
            }

            result.Attacks.Add(attack);
            result.TotalDamage += attack.DamageDealt;
        }

        return result;
    }
}

#region Result Types

public class PhaserResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int EnergyUsed { get; set; }
    public int ShipsDestroyed { get; set; }
    public List<PhaserHitResult> Hits { get; } = new();

    public static PhaserResult Failed(string message) => new() { Success = false, Message = message };
}

public class PhaserHitResult
{
    public Position TargetPosition { get; set; }
    public int DamageDealt { get; set; }
    public int RemainingShields { get; set; }
    public bool Destroyed { get; set; }
}

public class TorpedoResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public TorpedoHitType HitType { get; set; }
    public Position HitPosition { get; set; }
    public List<Position> Path { get; } = new();

    public static TorpedoResult Failed(string message) => new() { Success = false, Message = message };
}

public enum TorpedoHitType
{
    Miss,
    Klingon,
    Starbase,
    Star
}

public class KlingonAttackResult
{
    public List<KlingonAttack> Attacks { get; } = new();
    public int TotalDamage { get; set; }
    public bool EnterpriseDestroyed { get; set; }
}

public class KlingonAttack
{
    public Position AttackerPosition { get; set; }
    public int DamageDealt { get; set; }
    public int RemainingShields { get; set; }
    public bool BlockedByStarbase { get; set; }
    public string Message { get; set; } = string.Empty;
}

#endregion
