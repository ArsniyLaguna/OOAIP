using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class AngleTests
{
    [Fact]
    public void Angle_Addition_ReturnsCorrectSum_Criterion1()
    {
        Angle.Denominator = 8;
        var a1 = new Angle(5);
        var a2 = new Angle(7);

        var result = a1 + a2;

        Assert.Equal(4, result.Numerator);
    }

    [Fact]
    public void Angle_EqualsMethod_ReturnsTrueForEquivalentAngles_Criterion2()
    {
        Angle.Denominator = 8;
        var a1 = new Angle(15);
        var a2 = new Angle(23);

        Assert.True(a1.Equals(a2));
    }

    [Fact]
    public void Angle_OperatorEquals_ReturnsTrueForEquivalentAngles_Criterion3()
    {
        Angle.Denominator = 8;
        var a1 = new Angle(15);
        var a2 = new Angle(23);

        Assert.True(a1 == a2);
    }

    [Fact]
    public void Angle_EqualsMethod_ReturnsFalseForDifferentAngles_Criterion4()
    {
        Angle.Denominator = 8;
        var a1 = new Angle(1);
        var a2 = new Angle(2);

        Assert.False(a1.Equals(a2));
    }

    [Fact]
    public void Angle_OperatorNotEquals_ReturnsTrueForDifferentAngles_Criterion5()
    {

        Angle.Denominator = 8;
        var a1 = new Angle(1);
        var a2 = new Angle(2);

        Assert.True(a1 != a2);
    }

    [Fact]
    public void Angle_HasHashCode_Criterion6()
    {
        Angle.Denominator = 8;
        var angle = new Angle(5);

        Assert.NotEqual(0, angle.GetHashCode());
    }

    [Fact]
    public void Angle_MathCosAndSin_WorksDirectlyWithoutExplicitCast()
    {
        Angle.Denominator = 8;
        var angle = new Angle(0);

        double cosValue = Math.Cos(angle);
        double sinValue = Math.Sin(angle);

        Assert.Equal(1.0, cosValue, 5);
        Assert.Equal(0.0, sinValue, 5);
    }
}