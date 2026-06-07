using Moq;
using SpaceBattle.Lib;
using Xunit;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace SpaceBattle.Tests
{
    public class RegisterIoCRotateTests
    {
        [Fact]
        public void RegisterIoCDependencyRotateCommand_RegistersAndResolvesCorrectly()
        {
            IoC.Reset();
            
            IoC.Register("Adapters.IRotatable", args => new Mock<IRotatable>().Object);
            
            new RegisterIoCDependencyRotateCommand().Execute();
            
            var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", new Mock<object>().Object);
        
            Assert.NotNull(resolvedCommand);
            Assert.IsType<RotateCommand>(resolvedCommand);

            IoC.Reset();
        }
    }
}
