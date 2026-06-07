using System;
using System.Linq;

namespace SpaceBattle.Lib
{
    public sealed class Vector
    {
        private readonly int[] _coordinates;

        public int Size => _coordinates.Length;
        public int X => GetCoordinate(0);
        public int Y => GetCoordinate(1);

        public Vector(params int[] coordinates)
        {
            _coordinates = coordinates?.ToArray() ?? throw new ArgumentNullException(nameof(coordinates));
        }

        public int GetCoordinate(int index)
        {
            if (index < 0 || index >= Size)
                throw new IndexOutOfRangeException("Индекс вне границ вектора.");
            return _coordinates[index];
        }

        public static implicit operator Vector((int, int) tuple) => new Vector(tuple.Item1, tuple.Item2);

        // Перегрузка сложения
        public static Vector operator +(Vector a, Vector b)
        {
            if (a.Size != b.Size)
                throw new ArgumentException("Размерности векторов должны совпадать.");

            return new Vector(a._coordinates.Zip(b._coordinates, (x, y) => x + y).ToArray());
        }

        public override bool Equals(object? obj) => obj is Vector other && Equals(other);

        private bool Equals(Vector other) => _coordinates.SequenceEqual(other._coordinates);

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var coord in _coordinates) hash.Add(coord);
            return hash.ToHashCode();
        }

        public static bool operator ==(Vector? a, Vector? b) => Equals(a, b);
        public static bool operator !=(Vector? a, Vector? b) => !Equals(a, b);

        public override string ToString() => $"({string.Join(", ", _coordinates)})";
    }
}
