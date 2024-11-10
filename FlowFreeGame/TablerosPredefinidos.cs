namespace FlowFreeGame
{
    /// Clase que contiene los tableros predefinidos para el juego Flow Free.
    public static class TablerosPredefinidos
    {
        public static byte[,] Tablero2x2_1Color()
        {
            return new byte[,]
            {
                { 1, 0 },
                { 0, 1 }
            };
        }

        public static byte[,] Tablero3x3_1Color()
        {
            return new byte[,]
            {
                { 1, 0, 0 },
                { 0, 0, 0 },
                { 1, 0, 0 }
            };
        }

        public static byte[,] Tablero5x5_2Colores()
        {
            return new byte[,]
            {
                { 1, 0, 0, 0, 2 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 2 }
            };
        }

        public static byte[,] Tablero6x6_3Colores()
        {
            return new byte[,]
            {
                { 1, 0, 0, 0, 2, 0 },
                { 0, 1, 0, 0, 0, 2 },
                { 0, 0, 3, 4, 0, 0 },
                { 0, 0, 3, 0, 4, 0 },
                { 0, 5, 0, 0, 0, 0 },
                { 5, 0, 6, 0, 6, 0 }
            };
        }
    }
}
