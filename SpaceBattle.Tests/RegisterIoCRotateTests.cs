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
        
        var mockUObject = new Mock<object>();
        var mockAdapter = new Mock<IRotatable>();
        

        IoC.Register("Adapters.IRotatable", args => {
            var obj = args[0];
            return mockAdapter.Object;
        });
    
        new RegisterIoCDependencyRotateCommand().Execute();
        

        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", mockUObject.Object);
        
        // 6. Assert
        Assert.NotNull(resolvedCommand);
        Assert.IsType<RotateCommand>(resolvedCommand);
    }
}
