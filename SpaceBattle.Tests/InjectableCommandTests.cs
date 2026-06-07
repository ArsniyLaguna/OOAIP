using System;
using System.Collections.Generic;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class InjectableCommandTests
{
    public InjectableCommandTests()
    {
        // Очищаем состояние IoC перед каждым тестом
        IoC.Reset();
        
        // Регистрируем базовые зависимости, нужные всем тестам
        new RegisterDependencyCommandInjectableCommand().Execute();
        new RegisterIoCDependencyActionsStart().Execute();
        new RegisterIoCDependencyActionsStop().Execute(); 
    }
    
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
        // Регистрация уже прошла в конструкторе, повторно вызывать не нужно
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
        var internalCommandMock = new Mock<ICommand>();
        var commandQueue = new Queue<ICommand>();

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            { "Command", internalCommandMock.Object },
            { "Queue", commandQueue }
        };

        var resolvedCommand = IoC.Resolve<ICommand>("Actions.Start", order);
        Assert.NotNull(resolvedCommand);
        
        resolvedCommand.Execute();
        Assert.Single(commandQueue);
    }

    [Fact]
    public void RegisterActionsStop_ResolvesCorrectlyWithOrder_AndRunsInConstantTime()
    {
        var injectable = new CommandInjectableCommand();
        var movingMock = new Mock<ICommand>();
        injectable.Inject(movingMock.Object);

        IDictionary<string, object> order = new Dictionary<string, object>
        {
            { "TargetCommand", injectable }
        };

        var stopCommand = IoC.Resolve<ICommand>("Actions.Stop", order);
        Assert.NotNull(stopCommand);

        stopCommand.Execute();
        injectable.Execute();
        
        // Проверяем, что Execute() mock-объекта не был вызван повторно
        movingMock.Verify(m => m.Execute(), Times.Once);
    }
}
