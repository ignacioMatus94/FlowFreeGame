namespace FlowFreeGame
{
    public class Flujo
    {
        public Posicion UltimaPosicion { get; set; }
        public Posicion? PosicionAnterior { get; set; }

        public Flujo(Posicion ultimaPosicion)
        {
            UltimaPosicion = ultimaPosicion;
            PosicionAnterior = null;
        }
    }
}
