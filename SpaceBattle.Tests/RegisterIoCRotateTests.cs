using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterIoCRotateTests
{
[Fact]
public void RegisterIoCDependencyRotateCommand_RegistersAndResolvesCorrectly()
{
    IoC.Reset(); // Очищаем контейнер перед тестом
    
    var mockAdapter = new Mock<IRotatable>();
    var mockUObject = new Mock<object>();
    
    // Регистрируем адаптер напрямую
    IoC.Register("Adapters.IRotatable", args => mockAdapter.Object);
    
    var registerCommand = new RegisterIoCDependencyRotateCommand();
    registerCommand.Execute();
    
    // Act
    var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", mockUObject.Object);
    
    // Assert
    Assert.NotNull(resolvedCommand);
    Assert.IsType<RotateCommand>(resolvedCommand);
}
}
