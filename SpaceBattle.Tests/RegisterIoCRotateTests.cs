using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterIoCRotateTests
{
    [Fact]
    public void RegisterIoCDependencyRotateCommand_RegistersAndResolvesCorrectly()
    {
        IoC.Reset();

        IoC.Register("Adapters.IRotatable", args => new Mock<IRotatable>().Object);

        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", new Mock<object>().Object);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<RotateCommand>(resolvedCommand);
    }
}
