using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RotateTests
{
    [Fact]
    public void Rotate_ValidData_RotatesCorrectly_Criterion1()
    {
        Angle.Denominator = 8;
        var rotatableMock = new Mock<IRotatable>();

        rotatableMock.SetupGet(r => r.Angle).Returns(new Angle(1));
        rotatableMock.SetupGet(r => r.AngularVelocity).Returns(new Angle(1)); 

        var command = new RotateCommand(rotatableMock.Object);
        command.Execute();

        rotatableMock.VerifySet(r => r.Angle = new Angle(2), Times.Once);
    }

    [Fact]
    public void Rotate_MissingAngle_ThrowsException_Criterion2()
    {
        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupGet(r => r.Angle).Throws<Exception>();
        rotatableMock.SetupGet(r => r.AngularVelocity).Returns(new Angle(1));

        var command = new RotateCommand(rotatableMock.Object);

        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void Rotate_MissingVelocity_ThrowsException_Criterion3()
    {
        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupGet(r => r.Angle).Returns(new Angle(1));
        rotatableMock.SetupGet(r => r.AngularVelocity).Throws<Exception>();

        var command = new RotateCommand(rotatableMock.Object);

        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void Rotate_CannotSetAngle_ThrowsException_Criterion4()
    {
        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupGet(r => r.Angle).Returns(new Angle(1));
        rotatableMock.SetupGet(r => r.AngularVelocity).Returns(new Angle(1));
        
        rotatableMock.SetupSet(r => r.Angle = It.IsAny<Angle>()).Throws<Exception>();

        var command = new RotateCommand(rotatableMock.Object);

        Assert.Throws<Exception>(() => command.Execute());
    }
}
