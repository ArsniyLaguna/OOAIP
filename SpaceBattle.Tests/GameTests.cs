using System.Linq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class GameTests
{
    [Fact]
    public void GameTick_ShouldExecuteShootCommand_AndThenMovePhoton()
    {
        var repository = new GameObjectRepository();
        var factory = new MovementCommandFactory(repository);
        var game = new Game(repository, factory);

        var spaceship = new Spaceship(1, (10, 20));
        repository.Add(spaceship);

        // Игрок отдаёт приказ на выстрел вправо (1, 0)
        var fireCommand = new FirePhotonCommand(spaceship, (1, 0), repository);
        game.InjectCommand(fireCommand);

        game.Tick(); // Выстрел создаст торпеду, и она сразу сдвинется физикой игры

        // Assert
        var photon = repository.GetAll().OfType<Photon>().FirstOrDefault();
        Assert.NotNull(photon);
        // Начальная позиция торпеды (10, 20) + скорость 1 вправо = (11, 20)
        Assert.Equal((11, 20), photon.Position);
    }
}
