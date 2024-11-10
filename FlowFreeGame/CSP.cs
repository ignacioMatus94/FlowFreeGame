using System;

namespace FlowFreeGame
{
    public class CSP
    {
        public void ResolverCSP(Tablero tablero)
        {
            if (tablero == null)
            {
                Console.WriteLine("El tablero proporcionado es null.");
                return;
            }

            // Imprimir el tablero
            tablero.Imprimir();

            // Verificar si todos los flujos están conectados
            if (tablero.TodosFlujosConectados())
            {
                Console.WriteLine("Todos los flujos están conectados.");
            }
            else
            {
                Console.WriteLine("No todos los flujos están conectados.");
            }

            // Verificar si es una solución válida
            if (tablero.EsSolucionValida())
            {
                Console.WriteLine("Es una solución válida.");
            }
            else
            {
                Console.WriteLine("No es una solución válida.");
            }

            // Obtener movimientos posibles
            var movimientos = tablero.ObtenerMovimientosPosibles();
            Console.WriteLine($"Número de movimientos posibles: {movimientos.Count}");

            // Verificar si tiene bloqueos
            if (tablero.TieneBloqueos())
            {
                Console.WriteLine("El tablero tiene bloqueos.");
            }
            else
            {
                Console.WriteLine("El tablero no tiene bloqueos.");
            }
            object obj = tablero.ObtenerMovimientosPosibles(); 
        }
    }
}
