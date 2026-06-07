using System;
using SpaceBattle.Lib;

namespace SpaceBattle.Lib
{
    public class PhotonMovementCommand : ICommand
    {
        private readonly IMovable _movable;

        /// <summary>
        /// Команда для перемещения объекта, реализующего IMovable.
        /// </summary>
        /// <param name="movable">Объект для перемещения.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если movable равен null.</exception>
        public PhotonMovementCommand(IMovable movable)
        {
            // Используем оператор объединения с null для инициализации и выброса исключения
            _movable = movable ?? throw new ArgumentNullException(nameof(movable));
        }

        public void Execute()
        {
            _movable.Update();
        }
    }
}
