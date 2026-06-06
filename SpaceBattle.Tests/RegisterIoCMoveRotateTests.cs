using Moq;
using SpaceBattle.Lib;
using Xunit;
using System;
using System.Collections.Generic;

namespace SpaceBattle.Tests;

public class RegisterIoCMoveRotateTests
{
    [Fact]
    public void RegisterIoCDependencyMacroMoveRotate_RegistersAndResolvesBothCorrectly()
    {
        var moveSubCmdMock = new Mock<ICommand>();
        var rotateSubCmdMock = new Mock<ICommand>();

        // Используем Func вместо Mock<IStrategy>
        IoC.Register("Specs.Move", (args) => new List<string> { "MoveSubCommand" });
        IoC.Register("Specs.Rotate", (args) => new List<string> { "RotateSubCommand" });
        IoC.Register("MoveSubCommand", (args) => moveSubCmdMock.Object);
        IoC.Register("RotateSubCommand", (args) => rotateSubCmdMock.Object);

        var registrationCommand = new RegisterIoCDependencyMacroMoveRotate();
        registrationCommand.Execute();

        var resolvedMacroMove = IoC.Resolve<ICommand>("Macro.Move", new object[] { "игровой_объект" });
        var resolvedMacroRotate = IoC.Resolve<ICommand>("Macro.Rotate", new object[] { "игровой_объект" });

        Assert.NotNull(resolvedMacroMove);
        Assert.NotNull(resolvedMacroRotate);

        resolvedMacroMove.Execute();
        resolvedMacroRotate.Execute();

        moveSubCmdMock.Verify(c => c.Execute(), Times.Once);
        rotateSubCmdMock.Verify(c => c.Execute(), Times.Once);
    }
}
