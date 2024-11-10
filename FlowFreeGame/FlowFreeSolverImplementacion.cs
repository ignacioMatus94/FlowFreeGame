using System;
using System.Diagnostics;

namespace FlowFreeGame
{
    public static class FlowFreeSolverImplementacion
    {
        public static bool ResolverIDS(Tablero tableroInicial, int profundidadMax, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var estadoInicial = new Estado(tableroInicial);
                bool encontrado = Solver.IDS(estadoInicial, profundidadMax, out nodosExplorados);
                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                return encontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ResolverIDS: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static bool ResolverAStar(Tablero tableroInicial, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var estadoInicial = new Estado(tableroInicial);
                bool encontrado = Solver.AStar(estadoInicial, out nodosExplorados, out tiempoTotal);
                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                return encontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ResolverAStar: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static bool ResolverIDAEstrella(Tablero tableroInicial, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var estadoInicial = new Estado(tableroInicial);
                bool encontrado = Solver.IDAEstrella(estadoInicial, out nodosExplorados, out tiempoTotal);
                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                return encontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ResolverIDAEstrella: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static bool ResolverBeamSearch(Tablero tableroInicial, int ancho, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var estadoInicial = new Estado(tableroInicial);
                bool encontrado = Solver.BeamSearch(estadoInicial, ancho, out nodosExplorados);
                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                return encontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ResolverBeamSearch: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static bool ResolverCSP(Tablero tableroInicial, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var estadoInicial = new Estado(tableroInicial);
                bool encontrado = Solver.CSP(tableroInicial, out nodosExplorados, out tiempoTotal);
                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                return encontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ResolverCSP: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
