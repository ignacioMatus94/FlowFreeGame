using System;

namespace FlowFreeGame
{
    public class Posicion : IEquatable<Posicion>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Posicion(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Posicion? other)
        {
            if (other == null)
                return false;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Posicion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
