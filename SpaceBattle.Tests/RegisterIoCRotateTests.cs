using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterIoCRotateTests
{
    [Fact]
    public void RegisterIoCDependencyRotateCommand_RegistersAndResolvesCorrectly()
    {
        // 1. Очистка контейнера
        IoC.Reset();
        
        // 2. Подготовка объектов
        var mockAdapter = new Mock<IRotatable>();
        var mockUObject = new Mock<object>();
        
        // 3. Регистрируем адаптер как фабрику, которая принимает объект (uObject) 
        // и возвращает готовый адаптер
        IoC.Register("Adapters.IRotatable", args => {
            // В args[0] придет объект, который мы передадим в RotateCommand
            return mockAdapter.Object;
        });
        
        // 4. Выполняем регистрацию самой команды Rotate
        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();
        
        // 5. Act: Резолвим команду
        // Передаем mockUObject.Object, который внутри RotateCommand будет использован для получения адаптера
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", mockUObject.Object);
        
        // 6. Assert
        Assert.NotNull(resolvedCommand);
        Assert.IsType<RotateCommand>(resolvedCommand);
    }
}
