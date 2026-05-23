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
        ICommand registerCmd = new RegisterDependencyCommandInjectableCommand();
        registerCmd.Execute();

        var resolveAsICommand = IoC.Resolve<ICommand>("Commands.CommandInjectable");
        var resolveAsInjectable = IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var resolveAsClass = IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");

        Assert.NotNull(resolveAsICommand);
        Assert.NotNull(resolveAsInjectable);
        Assert.NotNull(resolveAsClass);
    }

    [Fact]
    public void RegisterActionsStart_ResolvesCorrectlyWithOrder()
    {
        ICommand registerCmd = new RegisterIoCDependencyActionsStart();
        registerCmd.Execute();

        IDictionary<string, object> order = new Dictionary<string, object>();

        var resolvedCommand = IoC.Resolve<ICommand>("Actions.Start", order);

        Assert.NotNull(resolvedCommand);
    }

    [Fact]
    public void RegisterActionsStop_ResolvesCorrectlyWithOrder_AndRunsInConstantTime()
    {
        ICommand registerCmd = new RegisterIoCDependencyActionsStop();
        registerCmd.Execute();
        IDictionary<string, object> order = new Dictionary<string, object>();
        var resolvedCommand = IoC.Resolve<ICommand>("Actions.Stop", order);
        Assert.NotNull(resolvedCommand);

        resolvedCommand.Execute();
    }

}
