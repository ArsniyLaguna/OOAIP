using System;

namespace SpaceBattle.Lib
{
    public class Photon : IMovable, IGameObject
    {
        private readonly Vector _velocity;

        public int Id { get; }
        public Vector Position { get; set; }
        public Vector Velocity => _velocity;

        public Photon(int id, Vector position, Vector direction, int speed = 1)
        {
            if (speed <= 0)
                throw new ArgumentException("Speed must be greater than 0", nameof(speed));
            
            Id = id;
            Position = position;
            _velocity = new Vector(direction.X * speed, direction.Y * speed);
        }

        public void Update()
        {
            Position = new Vector(Position.X + _velocity.X, Position.Y + _velocity.Y);
        }
    }
}
