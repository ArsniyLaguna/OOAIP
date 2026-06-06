using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBattle.Lib;


public class FirePhotonCommand : ICommand
{
    private readonly IGameObject _spaceship;
    private readonly (int X, int Y) _direction;
    private readonly IGameObjectRepository _repository;
    private readonly Queue<ICommand> _gameQueue;

    public FirePhotonCommand(IGameObject spaceship, (int X, int Y) direction, IGameObjectRepository repository, Queue<ICommand> gameQueue)
    {
        _spaceship = spaceship ?? throw new ArgumentNullException(nameof(spaceship));
        _direction = direction;
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _gameQueue = gameQueue ?? throw new ArgumentNullException(nameof(gameQueue));
    }

    public void Execute()
    {
        int nextId = _repository.GetAll().Count() + 1;
        var photon = new Photon(nextId, _spaceship.Position, _direction);

        _repository.Add(photon);

        var movementFactory = new MovementCommandFactory(_repository);
        var moveCommand = movementFactory.CreateMovementCommand(photon);

        _gameQueue.Enqueue(moveCommand);
    }
}
