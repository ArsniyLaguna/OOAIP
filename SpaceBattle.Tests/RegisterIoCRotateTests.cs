using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterIoCRotateTests
{
    [Fact]
    public void RegisterIoCDependencyRotateCommand_RegistersAndResolvesCorrectly()
    {
        // 1. Очистка контейнера, чтобы избежать конфликтов с другими тестами
        IoC.Reset();

        // 2. РЕГИСТРАЦИЯ КОМАНДЫ (инициализация контейнера)
        // Команда должна сама зарегистрировать в IoC ключ "Commands.Rotate"
        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        // 3. Подготовка объекта (UObject), который будет вращаться
        var mockUObject = new Mock<object>();

        // 4. Важно: для работы RotateCommand, контейнер должен уметь создавать IRotatable (адаптер)
        // Обычно RotateCommand внутри себя делает IoC.Resolve<IRotatable>("Adapters.IRotatable", uObject)
        IoC.Register("Adapters.IRotatable", args => {
            var mockAdapter = new Mock<IRotatable>();
            return mockAdapter.Object;
        });

        // 5. Act: Резолвим команду Rotate
        // Передаем uObject, так как именно он нужен команде для инициализации
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", mockUObject.Object);

        // 6. Assert
        Assert.NotNull(resolvedCommand);
        Assert.IsType<RotateCommand>(resolvedCommand);
    }
}
