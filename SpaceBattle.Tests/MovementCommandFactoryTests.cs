using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MovementCommandFactoryTests
{
    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new MovementCommandFactory(null));
    }

    [Fact]
    public void CreateMovementCommandsForAll_EmptyRepository_ShouldReturnEmptyList()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var factory = new MovementCommandFactory(repository);

        // Act
        var commands = factory.CreateMovementCommandsForAll().ToList();

        // Assert
        Assert.Empty(commands);
    }

    [Fact]
    public void CreateMovementCommandsForAll_MultipleObjects_ShouldReturnCommandsForEach()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var obj1 = new Photon(1, new Vector(0, 0), new Vector(1, 0), 1);
        var obj2 = new Photon(2, new Vector(5, 5), new Vector(0, 1), 1);
        repository.Add(obj1);
        repository.Add(obj2);
        var factory = new MovementCommandFactory(repository);

        // Act
        var commands = factory.CreateMovementCommandsForAll().ToList();

        // Assert
        Assert.Equal(2, commands.Count);
    }

    [Fact]
    public void CreateMovementCommand_ExistingObject_ShouldReturnCommand()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var photon = new Photon(1, new Vector(0, 0), new Vector(1, 0), 1);
        repository.Add(photon);
        var factory = new MovementCommandFactory(repository);

        // Act
        var command = factory.CreateMovementCommand(1);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<GameObjectMovementCommand>(command);
    }

    [Fact]
    public void CreateMovementCommand_NonExistingObject_ShouldThrowArgumentException()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var factory = new MovementCommandFactory(repository);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => factory.CreateMovementCommand(999));
    }

    [Fact]
    public void CreateMovementCommandsForAll_CommandsShouldMoveObjects()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var obj1 = new Photon(1, new Vector(0, 0), new Vector(1, 0), 2);
        var obj2 = new Photon(2, new Vector(0, 0), new Vector(0, 1), 3);
        repository.Add(obj1);
        repository.Add(obj2);
        var factory = new MovementCommandFactory(repository);

        // Act
        var commands = factory.CreateMovementCommandsForAll().ToList();
        foreach (var command in commands)
        {
            command.Execute();
        }

        // Assert
        Assert.Equal(new Vector(2, 0), obj1.Position);
        Assert.Equal(new Vector(0, 3), obj2.Position);
    }

    [Fact]
    public void CreateMovementCommand_ShouldCreateCorrectCommandType()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(0, 0));
        repository.Add(spaceship);
        var factory = new MovementCommandFactory(repository);

        // Act
        var command = factory.CreateMovementCommand(1);

        // Assert
        Assert.IsType<GameObjectMovementCommand>(command);
    }

    [Fact]
    public void CreateMovementCommandsForAll_WithMixedObjects_ShouldReturnCommandsForAll()
    {
        // Arrange
        var repository = new GameObjectRepository();
        var spaceship = new Spaceship(1, new Vector(10, 10));
        var photon = new Photon(2, new Vector(0, 0), new Vector(1, 1), 1);
        repository.Add(spaceship);
        repository.Add(photon);
        var factory = new MovementCommandFactory(repository);

        // Act
        var commands = factory.CreateMovementCommandsForAll().ToList();

        // Assert
        Assert.Equal(2, commands.Count);
    }
}
