using System;
using System.Diagnostics;

namespace ImageProcessing
{
    [DebuggerDisplay("R{Red}G{Green}B{Blue}")]
    public struct Pixel : IEquatable<Pixel>
    {
        public Pixel(int blue, int green, int red)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        public bool Equals(Pixel other)
        {
            return Red == other.Red && Green == other.Green && Blue == other.Blue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Pixel other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Red;
                hashCode = (hashCode * 397) ^ Green;
                hashCode = (hashCode * 397) ^ Blue;
                return hashCode;
            }
        }

        public static bool operator ==(Pixel left, Pixel right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Pixel left, Pixel right)
        {
            return !left.Equals(right);
        }
    }
}