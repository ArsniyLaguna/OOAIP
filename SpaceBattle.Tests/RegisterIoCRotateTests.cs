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
            // 1. Очистка всегда в самом начале
            IoC.Reset();
            
            // 2. Сначала регистрируем ВСЕ зависимости
            IoC.Register("Adapters.IRotatable", args => new Mock<IRotatable>().Object);
            
            // 3. Регистрируем команду
            new RegisterIoCDependencyRotateCommand().Execute();
            
            // 4. Резолвим
            var resolvedCommand = IoC.Resolve<ICommand>("Commands.Rotate", new Mock<object>().Object);
            
            // 5. Проверка
            Assert.NotNull(resolvedCommand);
            Assert.IsType<RotateCommand>(resolvedCommand);
            
            // 6. Очистка после теста
            IoC.Reset();
        }
    }
}
