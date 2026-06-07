using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MoveCommandTests
{
    [Fact]
    public void Move_ValidPositionAndVelocity_ChangesPositionCorrectly()
    {
        // Тест 1: Точка (12, 5), Скорость (-4, 1) => Точка (8, 6)
        var movingObjectMock = new Mock<IMovingObject>();
        
        movingObjectMock.SetupProperty(m => m.Position, new Vector(12, 5));
        movingObjectMock.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));

        ICommand moveCommand = new MoveCommand(movingObjectMock.Object);

        moveCommand.Execute();

        Assert.Equal(new Vector(8, 6), movingObjectMock.Object.Position);
    }

    [Fact]
    public void Move_CannotGetPosition_ThrowsException()
    {
        // Тест 2: Невозможно определить местонахождение (генерация исключения при чтении)
        var movingObjectMock = new Mock<IMovingObject>();
        
        movingObjectMock.SetupGet(m => m.Position).Throws<Exception>();
        movingObjectMock.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));

        ICommand moveCommand = new MoveCommand(movingObjectMock.Object);

        Assert.Throws<Exception>(() => moveCommand.Execute());
    }

    [Fact]
    public void Move_CannotGetVelocity_ThrowsException()
    {
        // Тест 3: Невозможно определить скорость
        var movingObjectMock = new Mock<IMovingObject>();
        
        movingObjectMock.SetupProperty(m => m.Position, new Vector(12, 5));
        movingObjectMock.SetupGet(m => m.Velocity).Throws<Exception>();

        ICommand moveCommand = new MoveCommand(movingObjectMock.Object);

        Assert.Throws<Exception>(() => moveCommand.Execute());
    }

    [Fact]
    public void Move_CannotSetPosition_ThrowsException()
    {
        // Тест 4: Невозможно изменить местонахождение (ошибка при записи)
        var movingObjectMock = new Mock<IMovingObject>();
        
        movingObjectMock.SetupGet(m => m.Position).Returns(new Vector(12, 5));
        movingObjectMock.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));
        
        // Настраиваем сеттер так, чтобы он выбрасывал ошибку при попытке записать значение
        movingObjectMock.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws<Exception>();

        ICommand moveCommand = new MoveCommand(movingObjectMock.Object);

        Assert.Throws<Exception>(() => moveCommand.Execute());
    }
}