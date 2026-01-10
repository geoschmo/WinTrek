using WinTrek.Core.Enums;
using WinTrek.Core.Models;
using WinTrek.Core.Services;

namespace WinTrek.Core.Tests.Services;

public class NavigationServiceTests
{
    private readonly NavigationService _service;
    private readonly Random _random;

    public NavigationServiceTests()
    {
        _random = new Random(42); // Seeded for deterministic tests
        _service = new NavigationService(_random);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 1.0)]   // East
    [InlineData(0, 0, 0, 1, 7.0)]   // North (down in grid)
    [InlineData(1, 0, 0, 0, 5.0)]   // West
    [InlineData(0, 1, 0, 0, 3.0)]   // South (up in grid)
    public void ComputeDirection_CardinalDirections_ReturnsCorrectValue(
        int x1, int y1, int x2, int y2, double expected)
    {
        var from = new Position(x1, y1);
        var to = new Position(x2, y2);

        double result = NavigationService.ComputeDirection(from, to);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateEnergyRequired_ReturnsWarpFactorTimes8()
    {
        Assert.Equal(8, NavigationService.CalculateEnergyRequired(1.0));
        Assert.Equal(16, NavigationService.CalculateEnergyRequired(2.0));
        Assert.Equal(64, NavigationService.CalculateEnergyRequired(8.0));
    }

    [Fact]
    public void GetMaxWarpFactor_NoDamage_Returns8()
    {
        double max = _service.GetMaxWarpFactor(0);
        Assert.Equal(8.0, max);
    }

    [Fact]
    public void GetMaxWarpFactor_WithDamage_ReturnsLimitedValue()
    {
        double max = _service.GetMaxWarpFactor(3);
        Assert.True(max < 8.0);
        Assert.True(max >= 0.2);
    }

    [Fact]
    public void Navigate_InvalidDirection_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        var galaxy = CreateTestGalaxy();
        var quadrant = galaxy[enterprise.QuadrantPosition];

        var result = _service.Navigate(enterprise, galaxy, quadrant, 0.5, 1.0);

        Assert.False(result.Success);
        Assert.Contains("Invalid course", result.Message);
    }

    [Fact]
    public void Navigate_InvalidWarpFactor_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        var galaxy = CreateTestGalaxy();
        var quadrant = galaxy[enterprise.QuadrantPosition];

        var result = _service.Navigate(enterprise, galaxy, quadrant, 1.0, 10.0);

        Assert.False(result.Success);
        Assert.Contains("Invalid warp factor", result.Message);
    }

    [Fact]
    public void Navigate_InsufficientEnergy_ReturnsFailed()
    {
        var enterprise = CreateTestEnterprise();
        enterprise.Energy = 10;
        var galaxy = CreateTestGalaxy();
        var quadrant = galaxy[enterprise.QuadrantPosition];

        var result = _service.Navigate(enterprise, galaxy, quadrant, 1.0, 5.0);

        Assert.False(result.Success);
        Assert.Contains("Insufficient energy", result.Message);
    }

    [Fact]
    public void Navigate_ValidMove_ConsumesEnergy()
    {
        var enterprise = CreateTestEnterprise();
        int initialEnergy = enterprise.Energy;
        var galaxy = CreateTestGalaxy();
        var quadrant = galaxy[enterprise.QuadrantPosition];

        var result = _service.Navigate(enterprise, galaxy, quadrant, 1.0, 1.0);

        Assert.True(result.Success);
        Assert.Equal(8, result.EnergyUsed);
        Assert.Equal(initialEnergy - 8, enterprise.Energy);
    }

    [Fact]
    public void IsDockingLocation_AdjacentToStarbase_ReturnsTrue()
    {
        var quadrant = new Quadrant { HasStarbase = true };
        quadrant[3, 3] = SectorContent.Starbase;

        // Check all adjacent positions
        Assert.True(NavigationService.IsDockingLocation(quadrant, new Position(2, 2)));
        Assert.True(NavigationService.IsDockingLocation(quadrant, new Position(3, 2)));
        Assert.True(NavigationService.IsDockingLocation(quadrant, new Position(4, 4)));
    }

    [Fact]
    public void IsDockingLocation_NotAdjacentToStarbase_ReturnsFalse()
    {
        var quadrant = new Quadrant { HasStarbase = true };
        quadrant[3, 3] = SectorContent.Starbase;

        Assert.False(NavigationService.IsDockingLocation(quadrant, new Position(0, 0)));
        Assert.False(NavigationService.IsDockingLocation(quadrant, new Position(6, 6)));
    }

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

    private Galaxy CreateTestGalaxy()
    {
        var galaxy = new Galaxy();
        galaxy.Initialize(new Random(42), GameData.QuadrantNames, 15, 3);
        return galaxy;
    }
}
