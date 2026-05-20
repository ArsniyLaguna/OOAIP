using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class VectorTests
{
    [Fact]
    public void VectorAddition_CorrectCoordinates_ReturnsSum()
    {
        var v1 = new Vector(1, -1, 2);
        var v2 = new Vector(-1, 1, -2);
        var expected = new Vector(0, 0, 0);

        var result = v1 + v2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void VectorAddition_DifferentDimensions_ThrowsArgumentException_FirstCase()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 2);

        Assert.Throws<ArgumentException>(() => v1 + v2);
    }

    [Fact]
    public void VectorAddition_DifferentDimensions_ThrowsArgumentException_SecondCase()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(1, 2, 3);

        Assert.Throws<ArgumentException>(() => v1 + v2);
    }

    [Fact]
    public void VectorEquals_SameCoordinatesDifferentObjects_ReturnsTrue()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 2, 3);

        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void VectorOperatorEquals_SameCoordinatesDifferentObjects_ReturnsTrue()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 2, 3);

        Assert.True(v1 == v2);
    }

    [Fact]
    public void VectorEquals_DifferentCoordinates_ReturnsFalse()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(4, 5, 6);

        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void VectorOperatorNotEquals_DifferentCoordinates_ReturnsTrue()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(4, 5, 6);

        Assert.True(v1 != v2);
    }

    [Fact]
    public void VectorGetHashCode_ChecksPresence()
    {
        var v = new Vector(1, 2, 3);
        int hashCode = v.GetHashCode();
        Assert.NotEqual(0, hashCode);
    }
}