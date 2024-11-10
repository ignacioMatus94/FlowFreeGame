using FlowFreeGame;

namespace FlowFreeSolver
{
    public class Variable
    {
        public byte Color { get; set; }
        public Posicion Posicion { get; set; }

        public Variable(byte color, Posicion posicion)
        {
            Color = color;
            Posicion = posicion;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Variable);
        }

        public bool Equals(Variable? other)
        {
            if (other == null)
                return false;

            return this.Color == other.Color && this.Posicion.Equals(other.Posicion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color, Posicion);
        }
    }
}
