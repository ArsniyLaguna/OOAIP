using System;
using System.Collections.Generic;

namespace SpaceBattle.Lib;

public class Game
{
    private readonly Queue<ICommand> _commandQueue = new();
    private readonly IGameObjectRepository _repository;
    private readonly MovementCommandFactory _movementFactory;

    public Game(IGameObjectRepository repository, MovementCommandFactory movementFactory)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _movementFactory = movementFactory ?? throw new ArgumentNullException(nameof(movementFactory));
    }

    public void InjectCommand(ICommand command)
    {
        _commandQueue.Enqueue(command);
    }

    public void Tick()
    {
        while (_commandQueue.Count > 0)
        {
            var command = _commandQueue.Dequeue();
            try
            {
                command.Execute();
            }
            catch (Exception)
            {

            }
        }

        var moveCommands = _movementFactory.CreateMovementCommandsForAll();
        foreach (var moveCmd in moveCommands)
        {
            moveCmd.Execute();
        }
    }
}
