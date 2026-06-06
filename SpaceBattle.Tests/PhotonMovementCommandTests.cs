using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class PhotonMovementCommandTests
{
    [Fact]
    public void Execute_ShouldMovePhoton()
    {
        // Arrange
        var photon = new Photon(1, (0, 0), (1, 1), 5);
        var command = new PhotonMovementCommand(photon);
        var initialPosition = photon.Position;

        // Act
        command.Execute();

        // Assert
        Assert.Equal((5, 5), photon.Position);
        Assert.NotEqual(initialPosition, photon.Position);
    }

    [Fact]
    public void Execute_MultipleExecutions_ShouldMovePhotonMultipleTimes()
    {
        // Arrange
        var photon = new Photon(1, (0, 0), (2, 0), 3);
        var command = new PhotonMovementCommand(photon);

        // Act
        command.Execute();
        command.Execute();
        command.Execute();

        // Assert
        Assert.Equal((18, 0), photon.Position);
    }

    [Fact]
    public void Execute_NegativeDirection_ShouldMoveInNegativeDirection()
    {
        // Arrange
        var photon = new Photon(1, (10, 10), (-1, -1), 2);
        var command = new PhotonMovementCommand(photon);

        // Act
        command.Execute();

        // Assert
        Assert.Equal((8, 8), photon.Position);
    }

    [Fact]
    public void Constructor_NullPhoton_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new PhotonMovementCommand(null));
    }

    [Fact]
    public void Execute_WithZeroDirection_ShouldNotMove()
    {
        // Arrange
        var photon = new Photon(1, (5, 5), (0, 0), 10);
        var command = new PhotonMovementCommand(photon);

        // Act
        command.Execute();

        // Assert
        Assert.Equal((5, 5), photon.Position);
    }
}
