using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FlowFreeGame
{
    public static class Solver
    {
        /// Implementación del algoritmo Beam Search.
        public static bool BeamSearch(Estado estadoInicial, int ancho, out int nodosExplorados)
        {
            nodosExplorados = 0;
            List<Estado> beam = new List<Estado> { estadoInicial };

            Console.WriteLine($"Iniciando Beam Search con ancho: {ancho}");

            while (beam.Count > 0)
            {
                Console.WriteLine($"Beam size actual: {beam.Count}");
                List<Estado> siguienteBeam = new List<Estado>();

                foreach (var estado in beam)
                {
                    nodosExplorados++;

                    if (estado.EsObjetivo())
                    {
                        Console.WriteLine("¡Solución encontrada!");
                        estado.Tablero.Imprimir();
                        return true;
                    }

                    var sucesores = estado.GenerarSucesores();

                    Console.WriteLine($"Generados {sucesores.Count} sucesores para este nodo.");

                    foreach (var sucesor in sucesores)
                    {
                        siguienteBeam.Add(sucesor);
                    }
                }

                siguienteBeam = siguienteBeam.OrderBy(s => s.CostoF).Take(ancho).ToList();
                Console.WriteLine($"Beam size siguiente nivel: {siguienteBeam.Count}");
                beam = siguienteBeam;
            }

            Console.WriteLine("No se encontró una solución.");
            return false;
        }

        /// Implementación del algoritmo IDS (Iterative Deepening Search).
        public static bool IDS(Estado estadoInicial, int profundidadMaxima, out int nodosExplorados)
        {
            nodosExplorados = 0;

            for (int profundidad = 0; profundidad <= profundidadMaxima; profundidad++)
            {
                int nodosExploradosNivel = 0;
                Console.WriteLine($"IDS - Profundidad: {profundidad}");
                bool encontrado = DLS(estadoInicial, profundidad, ref nodosExploradosNivel);
                nodosExplorados += nodosExploradosNivel;

                if (encontrado)
                {
                    Console.WriteLine($"¡Solución encontrada en profundidad {profundidad}!");
                    return true;
                }
            }

            Console.WriteLine("No se encontró una solución dentro del límite de profundidad.");
            return false;
        }

        /// Depth-Limited Search utilizado por IDS.
        private static bool DLS(Estado estado, int profundidad, ref int nodosExplorados)
        {
            nodosExplorados++;

            if (estado.EsObjetivo())
            {
                Console.WriteLine("¡Solución encontrada!");
                estado.Tablero.Imprimir();
                return true;
            }

            if (profundidad == 0)
                return false;

            var sucesores = estado.GenerarSucesores();
            Console.WriteLine($"Generados {sucesores.Count} sucesores para este nodo en profundidad {profundidad}.");

            foreach (var sucesor in sucesores)
            {
                bool encontrado = DLS(sucesor, profundidad - 1, ref nodosExplorados);
                if (encontrado)
                    return true;
            }

            return false;
        }

        /// Implementación del algoritmo A*.
        public static bool AStar(Estado estadoInicial, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var queue = new SortedSet<Estado>(Comparer<Estado>.Create((a, b) =>
                {
                    int compare = a.CostoF.CompareTo(b.CostoF);
                    if (compare == 0)
                        compare = a.Profundidad.CompareTo(b.Profundidad);
                    return compare;
                }));

                var estadosVisitados = new HashSet<Tablero>();
                queue.Add(estadoInicial);

                while (queue.Count > 0)
                {
                    var current = queue.First();
                    queue.Remove(current);

                    nodosExplorados++;

                    if (current.EsObjetivo())
                    {
                        cronometro.Stop();
                        tiempoTotal = cronometro.Elapsed.TotalSeconds;
                        Console.WriteLine("¡Solución encontrada!");
                        current.Tablero.Imprimir();
                        return true;
                    }

                    estadosVisitados.Add(current.Tablero);

                    var sucesores = current.GenerarSucesores();

                    foreach (var sucesor in sucesores)
                    {
                        if (!estadosVisitados.Contains(sucesor.Tablero))
                        {
                            queue.Add(sucesor);
                        }
                    }
                }

                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                Console.WriteLine("No se encontró una solución.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en A*: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        /// Implementación del algoritmo IDA*.
        public static bool IDAEstrella(Estado estadoInicial, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                int limite = estadoInicial.CostoF;

                while (true)
                {
                    int nuevoLimite = int.MaxValue;
                    bool encontrado = DLS_Iterativo(estadoInicial, limite, ref nodosExplorados, ref nuevoLimite);

                    if (encontrado)
                    {
                        cronometro.Stop();
                        tiempoTotal = cronometro.Elapsed.TotalSeconds;
                        return true;
                    }

                    if (nuevoLimite == int.MaxValue)
                        break;

                    limite = nuevoLimite;
                }

                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                Console.WriteLine("No se encontró una solución.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en IDA*: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        /// Depth-Limited Search utilizado por IDA* con límite de costo.  
        private static bool DLS_Iterativo(Estado estado, int limite, ref int nodosExplorados, ref int nuevoLimite)
        {
            int costoActual = estado.CostoF;
            nodosExplorados++;

            if (costoActual > limite)
            {
                nuevoLimite = Math.Min(nuevoLimite, costoActual);
                return false;
            }

            if (estado.EsObjetivo())
            {
                Console.WriteLine("¡Solución encontrada!");
                estado.Tablero.Imprimir();
                return true;
            }

            var sucesores = estado.GenerarSucesores();

            foreach (var sucesor in sucesores)
            {
                bool encontrado = DLS_Iterativo(sucesor, limite, ref nodosExplorados, ref nuevoLimite);
                if (encontrado)
                    return true;
            }

            return false;
        }

        /// Implementación del algoritmo CSP (Constraint Satisfaction Problem).
        public static bool CSP(Tablero estadoInicial, out int nodosExplorados, out double tiempoTotal)
        {
            nodosExplorados = 0;
            tiempoTotal = 0;
            var cronometro = Stopwatch.StartNew();

            try
            {
                var queue = new Queue<Estado>();
                var estadosVisitados = new HashSet<Tablero>();

                var estado = new Estado(estadoInicial);
                queue.Enqueue(estado);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    nodosExplorados++;

                    if (current.EsObjetivo())
                    {
                        cronometro.Stop();
                        tiempoTotal = cronometro.Elapsed.TotalSeconds;
                        Console.WriteLine("¡Solución encontrada!");
                        current.Tablero.Imprimir();
                        return true;
                    }

                    estadosVisitados.Add(current.Tablero);

                    var sucesores = current.GenerarSucesores();

                    foreach (var sucesor in sucesores)
                    {
                        if (!estadosVisitados.Contains(sucesor.Tablero))
                        {
                            queue.Enqueue(sucesor);
                        }
                    }
                }

                cronometro.Stop();
                tiempoTotal = cronometro.Elapsed.TotalSeconds;
                Console.WriteLine("No se encontró una solución.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en CSP: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
