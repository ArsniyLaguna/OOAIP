using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class PhotonMovementCommandTests
{
    [Fact]
    public void Execute_ShouldMovePhoton()
    {
        // Arrange
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 1), 5);
        var command = new PhotonMovementCommand(photon);
        var initialPosition = photon.Position;

        // Act
        command.Execute();

        // Assert
        Assert.Equal(new Vector(5, 5), photon.Position);
        Assert.NotEqual(initialPosition, photon.Position);
    }

    [Fact]
    public void Execute_MultipleExecutions_ShouldMovePhotonMultipleTimes()
    {
        // Arrange
        var photon = new Photon(1, new Vector(0, 0), new Vector(2, 0), 3);
        var command = new PhotonMovementCommand(photon);

        // Act
        command.Execute();
        command.Execute();
        command.Execute();

        // Assert
        Assert.Equal(new Vector(18, 0), photon.Position);
    }

    [Fact]
    public void Execute_NegativeDirection_ShouldMoveInNegativeDirection()
    {
        // Arrange
        var photon = new Photon(1, new Vector(10, 10), new Vector(-1, -1), 2);
        var command = new PhotonMovementCommand(photon);

        // Act
        command.Execute();

        // Assert
        Assert.Equal(new Vector(8, 8), photon.Position);
    }

    [Fact]
    public void Constructor_NullPhoton_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        // Добавлен оператор ! для подавления предупреждения Nullability
        Assert.Throws<ArgumentNullException>(() => new PhotonMovementCommand(null!));
    }

    [Fact]
    public void Execute_WithZeroDirection_ShouldNotMove()
    {
        // Arrange
        var photon = new Photon(1, new Vector(5, 5), new Vector(0, 0), 10);
        var command = new PhotonMovementCommand(photon);

        // Act
        command.Execute();

        // Assert
        Assert.Equal(new Vector(5, 5), photon.Position);
    }
}
