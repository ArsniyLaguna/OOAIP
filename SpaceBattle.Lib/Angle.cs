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

    public override bool Equals(object? obj)
    {
        if (obj is Angle other) return this.Numerator == other.Numerator;
        return false;
    }

    public override int GetHashCode() => Numerator.GetHashCode();
}