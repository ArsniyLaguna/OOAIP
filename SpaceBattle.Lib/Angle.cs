using System;

namespace SpaceBattle.Lib;

public class Angle
{
    public static int Denominator { get; set; } = 8;
    public int Numerator { get; }

    public Angle(int numerator)
    {
        Numerator = ((numerator % Denominator) + Denominator) % Denominator;
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
        var objectAngle = obj as Angle;

        return objectAngle is not null && Numerator == objectAngle.Numerator;
    }

    public override int GetHashCode()
    {
        return Numerator.GetHashCode();
    }

    public static bool operator ==(Angle? a, Angle? b)
    {
        return ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    }

    public static bool operator !=(Angle? a, Angle? b)
    {
        return !(a == b);
    }
}
