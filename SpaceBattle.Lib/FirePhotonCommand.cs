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
    private readonly Func<IGameObject, ICommand> _moveCommandFactory;


    public FirePhotonCommand(
        IGameObject spaceship, 
        (int X, int Y) direction, 
        IGameObjectRepository repository, 
        Queue<ICommand> gameQueue,
        Func<IGameObject, ICommand> moveCommandFactory)
    {
        _spaceship = spaceship ?? throw new ArgumentNullException(nameof(spaceship));
        _direction = direction;
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _gameQueue = gameQueue ?? throw new ArgumentNullException(nameof(gameQueue));
        _moveCommandFactory = moveCommandFactory ?? throw new ArgumentNullException(nameof(moveCommandFactory));
    }

    public void Execute()
    {
        // 1. Создаем торпеду (ID генерируем динамически на основе размера репозитория)
        int nextId = _repository.GetAll().Count() + 1;
        var photon = new Photon(nextId, _spaceship.Position, _direction);

        // 2. Связываем выстрел с репозиторием (добавляем торпеду в систему)
        _repository.Add(photon);

        // 3. Создаем команду движения через абстрактный делегат (чистый DIP)
        var moveCommand = _moveCommandFactory(photon);

        // 4. Запускаем торпеду: отправляем команду движения в общую очередь игры
        _gameQueue.Enqueue(moveCommand);
    }
}
