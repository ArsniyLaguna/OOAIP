using System;

namespace SpaceBattle.Lib
{
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

        public override bool Equals(object? obj)
        {
            return obj is Angle other && Numerator == other.Numerator;
        }

        public override int GetHashCode() => Numerator.GetHashCode();

        public static implicit operator double(Angle angle)
        {
            return (double)angle.Numerator / Denominator * 2 * Math.PI;
        }

        public static bool operator ==(Angle? a, Angle? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
        public static bool operator !=(Angle? a, Angle? b) => !(a == b);
    }
}
