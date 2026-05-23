namespace SpaceBattle.Lib;

public class Angle
{
    public static int Denominator { get; set; } = 8;

    public int Numerator { get; }

    public Angle(int numerator)
    {
        int maxUnits = Denominator; 
        int resolved = numerator % maxUnits;
        
        if (resolved < 0) resolved += maxUnits;
        
        Numerator = resolved;
    }

    public static Angle operator +(Angle a, Angle b)
    {
        return new Angle(a.Numerator + b.Numerator);
    }

    public static implicit operator double(Angle angle)
    {
        return (double)angle.Numerator / Denominator * 2 * Math.PI;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Angle other)
        {
            return this.Numerator == other.Numerator;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Numerator.GetHashCode();
    }

    public static bool operator ==(Angle? a, Angle? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Angle? a, Angle? b)
    {
        return !(a == b);
    }
}
