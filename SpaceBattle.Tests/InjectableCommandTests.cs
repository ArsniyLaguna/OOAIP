using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class InjectableCommandTests
{
    [Fact]
    public void InjectableCommand_WithInjectedCommand_ExecutesCorrectly()
    {
        var targetCommandMock = new Mock<ICommand>();
        var injectable = new CommandInjectableCommand();

        injectable.Inject(targetCommandMock.Object);
        injectable.Execute();

        targetCommandMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void InjectableCommand_WithoutInjectedCommand_ThrowsException()
    {
        var injectable = new CommandInjectableCommand();

        Assert.Throws<Exception>(() => injectable.Execute());
    }

    [Fact]
    public void RegisterCommandInjectable_ResolvesToAllRequiredTypes()
    {
        //Вызываем команду регистрации зависимости
        ICommand registerCmd = new RegisterDependencyCommandInjectableCommand();
        registerCmd.Execute();

        // Проверяем критерий приемки разрешение работает без исключений для всех трех типов
        var resolveAsICommand = IoC.Resolve<ICommand>("Commands.CommandInjectable");
        var resolveAsInjectable = IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var resolveAsClass = IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");

        // проверяем что объекты успешно создались
        Assert.NotNull(resolveAsICommand);
        Assert.NotNull(resolveAsInjectable);
        Assert.NotNull(resolveAsClass);
    }
}