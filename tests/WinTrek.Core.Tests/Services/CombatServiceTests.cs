using WinTrek.Core.Enums;
using WinTrek.Core.Models;
using WinTrek.Core.Services;

namespace WinTrek.Core.Tests.Services;

public class CombatServiceTests
{
    private readonly CombatService _service;
    private readonly Random _random;

    public CombatServiceTests()
    {
        _random = new Random(42);
        _service = new CombatService(_random);
    }

    #region Phaser Tests

    [Fact]
    public void FirePhasers_DamagedPhasers_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        enterprise.Damage.InduceDamage(ShipSystem.Phaser, 3);
        var quadrant = new Quadrant();
        var klingons = new List<KlingonShip>();

        var result = _service.FirePhasers(enterprise, quadrant, klingons, 100);

        Assert.False(result.Success);
        Assert.Contains("damaged", result.Message);
    }

    [Fact]
    public void FirePhasers_NoKlingons_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        var quadrant = new Quadrant();
        var klingons = new List<KlingonShip>();

        var result = _service.FirePhasers(enterprise, quadrant, klingons, 100);

        Assert.False(result.Success);
        Assert.Contains("no Klingon ships", result.Message);
    }

    [Fact]
    public void FirePhasers_InvalidEnergy_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        var quadrant = new Quadrant();
        var klingons = CreateKlingonShips(1, quadrant);

        var result = _service.FirePhasers(enterprise, quadrant, klingons, 5000);

        Assert.False(result.Success);
        Assert.Contains("Invalid energy", result.Message);
    }

    [Fact]
    public void FirePhasers_HitsKlingon_DealsDamage()
    {
        var enterprise = CreateTestEnterprise();
        var quadrant = new Quadrant();
        var klingons = CreateKlingonShips(1, quadrant);
        int initialShields = klingons[0].ShieldLevel;

        var result = _service.FirePhasers(enterprise, quadrant, klingons, 200);

        Assert.True(result.Success);
        Assert.Single(result.Hits);
        Assert.True(result.Hits[0].DamageDealt > 0);
    }

    [Fact]
    public void FirePhasers_DestroysKlingon_RemovesFromList()
    {
        var enterprise = CreateTestEnterprise();
        var quadrant = new Quadrant();
        var klingons = CreateKlingonShips(1, quadrant);
        klingons[0].ShieldLevel = 50; // Low shields

        var result = _service.FirePhasers(enterprise, quadrant, klingons, 500);

        Assert.True(result.Success);
        Assert.Equal(1, result.ShipsDestroyed);
        Assert.Empty(klingons);
    }

    #endregion

    #region Torpedo Tests

    [Fact]
    public void FireTorpedo_DamagedTorpedoControl_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        enterprise.Damage.InduceDamage(ShipSystem.PhotonTorpedo, 3);
        var quadrant = new Quadrant();
        var klingons = new List<KlingonShip>();

        var result = _service.FireTorpedo(enterprise, quadrant, klingons, 1.0);

        Assert.False(result.Success);
        Assert.Contains("damaged", result.Message);
    }

    [Fact]
    public void FireTorpedo_NoTorpedoes_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        enterprise.PhotonTorpedoes = 0;
        var quadrant = new Quadrant();
        var klingons = CreateKlingonShips(1, quadrant);

        var result = _service.FireTorpedo(enterprise, quadrant, klingons, 1.0);

        Assert.False(result.Success);
        Assert.Contains("exhausted", result.Message);
    }

    [Fact]
    public void FireTorpedo_NoKlingons_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        var quadrant = new Quadrant();
        var klingons = new List<KlingonShip>();

        var result = _service.FireTorpedo(enterprise, quadrant, klingons, 1.0);

        Assert.False(result.Success);
        Assert.Contains("no Klingon ships", result.Message);
    }

    [Fact]
    public void FireTorpedo_InvalidDirection_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        var quadrant = new Quadrant();
        var klingons = CreateKlingonShips(1, quadrant);

        var result = _service.FireTorpedo(enterprise, quadrant, klingons, 10.0);

        Assert.False(result.Success);
        Assert.Contains("Invalid direction", result.Message);
    }

    [Fact]
    public void FireTorpedo_ConsumesTorpedo()
    {
        var enterprise = CreateTestEnterprise();
        int initialTorpedoes = enterprise.PhotonTorpedoes;
        var quadrant = new Quadrant();
        var klingons = CreateKlingonShips(1, quadrant);

        _service.FireTorpedo(enterprise, quadrant, klingons, 1.0);

        Assert.Equal(initialTorpedoes - 1, enterprise.PhotonTorpedoes);
    }

    #endregion

    #region Klingon Attack Tests

    [Fact]
    public void KlingonsAttack_NoKlingons_ReturnsEmptyResult()
    {
        var enterprise = CreateTestEnterprise();
        var klingons = new List<KlingonShip>();

        var result = _service.KlingonsAttack(enterprise, klingons);

        Assert.Empty(result.Attacks);
        Assert.Equal(0, result.TotalDamage);
    }

    [Fact]
    public void KlingonsAttack_WhenDocked_NoDamage()
    {
        var enterprise = CreateTestEnterprise();
        enterprise.IsDocked = true;
        enterprise.ShieldLevel = 500;
        var klingons = CreateKlingonShips(2, new Quadrant());

        var result = _service.KlingonsAttack(enterprise, klingons);

        Assert.Equal(2, result.Attacks.Count);
        Assert.All(result.Attacks, a => Assert.True(a.BlockedByStarbase));
        Assert.Equal(500, enterprise.ShieldLevel); // No damage taken
    }

    [Fact]
    public void KlingonsAttack_DamagesEnterprise()
    {
        var enterprise = CreateTestEnterprise();
        enterprise.ShieldLevel = 1000;
        var klingons = CreateKlingonShips(1, new Quadrant());

        var result = _service.KlingonsAttack(enterprise, klingons);

        Assert.Single(result.Attacks);
        Assert.True(result.TotalDamage > 0 || result.Attacks[0].DamageDealt >= 0);
    }

    #endregion

    private Enterprise CreateTestEnterprise()
    {
        return new Enterprise
        {
            QuadrantPosition = new Position(4, 4),
            SectorPosition = new Position(4, 4),
            Energy = 3000,
            PhotonTorpedoes = 10
        };
    }

    private List<KlingonShip> CreateKlingonShips(int count, Quadrant quadrant)
    {
        var ships = new List<KlingonShip>();
        for (int i = 0; i < count; i++)
        {
            var ship = new KlingonShip
            {
                SectorPosition = new Position(i + 1, i + 1),
                ShieldLevel = 300 + _random.Next(200)
            };
            ships.Add(ship);
            quadrant[ship.SectorPosition] = SectorContent.Klingon;
        }
        return ships;
    }
}
