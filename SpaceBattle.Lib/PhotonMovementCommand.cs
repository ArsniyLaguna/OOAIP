using SpaceBattle.Lib;

namespace SpaceBattle.Lib
{
    public class PhotonMovementCommand : ICommand
    {
        private readonly IMovable _movable;

        public PhotonMovementCommand(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            _movable.Update();
        }
    }
}
