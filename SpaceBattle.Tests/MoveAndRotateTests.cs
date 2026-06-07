using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class MoveAndRotateTests
{
[Fact]
public void RegisterMoveCommand_ResolvesCorrectly()
{
    // 1. Создаем мок объекта, который умеет двигаться
    var movingObjectMock = new Mock<IMovingObject>();

    // 2. Регистрируем команду
    ICommand registerCmd = new RegisterIoCDependencyMoveCommand();
    registerCmd.Execute();

    // 3. Вызываем Resolve, передавая сам мок-объект в качестве аргумента
    var resolvedCommand = IoC.Resolve<ICommand>("Commands.Move", movingObjectMock.Object);

    // 4. Проверяем
    Assert.NotNull(resolvedCommand);
    Assert.IsType<MoveCommand>(resolvedCommand);
}
}
