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
}