namespace SpaceBattle.Lib;

public class Vector
{
    private readonly int[] _coordinates;

    public Vector(params int[] coordinates)
    {
        _coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
    }

    public int Size => _coordinates.Length;

    public int GetCoordinate(int index) => _coordinates[index];

    public static Vector operator +(Vector a, Vector b)
    {
        if (a.Size != b.Size)
            throw new ArgumentException("векторы должны иметь одинаковую размерность.");

        int[] result = new int[a.Size];
        for (int i = 0; i < a.Size; i++)
        {
            result[i] = a._coordinates[i] + b._coordinates[i];
        }
        return new Vector(result);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Vector other || Size != other.Size)
            return false;

        for (int i = 0; i < Size; i++)
        {
            if (_coordinates[i] != other._coordinates[i])
                return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var coord in _coordinates)
        {
            hash = hash * 23 + coord.GetHashCode();
        }
        return hash;
    }

    public static bool operator ==(Vector? a, Vector? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Vector? a, Vector? b)
    {
        return !(a == b);
    }
}
