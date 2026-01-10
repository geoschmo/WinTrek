using WinTrek.Core.Models;

namespace WinTrek.Core.Tests.Models;

public class PositionTests
{
    [Fact]
    public void DistanceTo_SamePosition_ReturnsZero()
    {
        var pos = new Position(3, 4);
        Assert.Equal(0, pos.DistanceTo(pos));
    }

    [Fact]
    public void DistanceTo_HorizontalDistance_ReturnsCorrectValue()
    {
        var pos1 = new Position(0, 0);
        var pos2 = new Position(3, 0);
        Assert.Equal(3, pos1.DistanceTo(pos2));
    }

    [Fact]
    public void DistanceTo_VerticalDistance_ReturnsCorrectValue()
    {
        var pos1 = new Position(0, 0);
        var pos2 = new Position(0, 4);
        Assert.Equal(4, pos1.DistanceTo(pos2));
    }

    [Fact]
    public void DistanceTo_DiagonalDistance_ReturnsCorrectValue()
    {
        var pos1 = new Position(0, 0);
        var pos2 = new Position(3, 4);
        Assert.Equal(5, pos1.DistanceTo(pos2)); // 3-4-5 triangle
    }

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(7, 7, true)]
    [InlineData(4, 4, true)]
    [InlineData(-1, 0, false)]
    [InlineData(0, -1, false)]
    [InlineData(8, 0, false)]
    [InlineData(0, 8, false)]
    public void IsValid_VariousPositions_ReturnsExpected(int x, int y, bool expected)
    {
        var pos = new Position(x, y);
        Assert.Equal(expected, pos.IsValid());
    }

    [Fact]
    public void ToDisplayString_ReturnsOneBased()
    {
        var pos = new Position(0, 0);
        Assert.Equal("[1,1]", pos.ToDisplayString());

        var pos2 = new Position(7, 7);
        Assert.Equal("[8,8]", pos2.ToDisplayString());
    }
}
