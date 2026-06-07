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

        var mockAdapter = new Mock<IRotatable>();
        var mockUObject = new Mock<object>();

        IoC.Register("Adapters.IRotatable", args =>
        {
            return mockAdapter.Object;
        });

        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", mockUObject.Object);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<RotateCommand>(resolvedCommand);
    }
}
