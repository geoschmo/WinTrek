using WinTrek.Core.Enums;
using WinTrek.Core.Models;

namespace WinTrek.Core.Tests.Models;

public class EnterpriseTests
{
    [Fact]
    public void NewEnterprise_HasDefaultValues()
    {
        var enterprise = new Enterprise();

        Assert.Equal(Enterprise.MaxEnergy, enterprise.Energy);
        Assert.Equal(Enterprise.MaxTorpedoes, enterprise.PhotonTorpedoes);
        Assert.Equal(0, enterprise.ShieldLevel);
        Assert.False(enterprise.IsDocked);
        Assert.False(enterprise.IsDestroyed);
    }

    [Fact]
    public void TransferShields_AddToShields_DeductsFromEnergy()
    {
        var enterprise = new Enterprise();
        int initialEnergy = enterprise.Energy;

        bool result = enterprise.TransferShields(500);

        Assert.True(result);
        Assert.Equal(initialEnergy - 500, enterprise.Energy);
        Assert.Equal(500, enterprise.ShieldLevel);
    }

    [Fact]
    public void TransferShields_RemoveFromShields_AddsToEnergy()
    {
        var enterprise = new Enterprise();
        enterprise.TransferShields(500); // Add shields first
        int energyAfterAdd = enterprise.Energy;

        bool result = enterprise.TransferShields(-200);

        Assert.True(result);
        Assert.Equal(energyAfterAdd + 200, enterprise.Energy);
        Assert.Equal(300, enterprise.ShieldLevel);
    }

    [Fact]
    public void TransferShields_InsufficientEnergy_ReturnsFalse()
    {
        var enterprise = new Enterprise();

        bool result = enterprise.TransferShields(enterprise.Energy + 100);

        Assert.False(result);
        Assert.Equal(Enterprise.MaxEnergy, enterprise.Energy);
        Assert.Equal(0, enterprise.ShieldLevel);
    }

    [Fact]
    public void TakeDamage_WithShields_ReducesShields()
    {
        var enterprise = new Enterprise();
        enterprise.TransferShields(500);

        bool destroyed = enterprise.TakeDamage(200);

        Assert.False(destroyed);
        Assert.Equal(300, enterprise.ShieldLevel);
        Assert.False(enterprise.IsDestroyed);
    }

    [Fact]
    public void TakeDamage_ExceedsShields_DestroysShip()
    {
        var enterprise = new Enterprise();
        enterprise.TransferShields(100);

        bool destroyed = enterprise.TakeDamage(200);

        Assert.True(destroyed);
        Assert.Equal(0, enterprise.ShieldLevel);
        Assert.True(enterprise.IsDestroyed);
    }

    [Fact]
    public void Dock_RestoresAllResources()
    {
        var enterprise = new Enterprise();
        enterprise.Energy = 100;
        enterprise.PhotonTorpedoes = 2;
        enterprise.ShieldLevel = 500;
        enterprise.Damage.InduceDamage(ShipSystem.Phaser, 3);

        enterprise.Dock();

        Assert.True(enterprise.IsDocked);
        Assert.Equal(Enterprise.MaxEnergy, enterprise.Energy);
        Assert.Equal(Enterprise.MaxTorpedoes, enterprise.PhotonTorpedoes);
        Assert.Equal(0, enterprise.ShieldLevel); // Shields lowered for docking
        Assert.False(enterprise.Damage.HasAnyDamage);
    }

    [Theory]
    [InlineData(1, AlertCondition.Red)]
    [InlineData(3, AlertCondition.Red)]
    [InlineData(0, AlertCondition.Green)]
    public void GetCondition_WithKlingons_ReturnsRed(int klingons, AlertCondition expected)
    {
        var enterprise = new Enterprise();
        Assert.Equal(expected, enterprise.GetCondition(klingons));
    }

    [Fact]
    public void GetCondition_LowEnergy_ReturnsYellow()
    {
        var enterprise = new Enterprise { Energy = 200 };
        Assert.Equal(AlertCondition.Yellow, enterprise.GetCondition(0));
    }
}
