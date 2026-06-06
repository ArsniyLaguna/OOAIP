public interface IGameObject
{
    int Id { get; }
    (int X, int Y) Position { get; set; }
}
