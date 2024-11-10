// Estado.cs
using System;

namespace FlowFreeGame
{
    
    /// Representa un estado en el árbol de búsqueda.
    public class Estado : IComparable<Estado>, IEquatable<Estado>
    {
      
        /// Tablero actual del estado.
        public Tablero Tablero { get; set; }

      
        /// Estado padre (estado desde el cual se generó este estado).
        public Estado? Padre { get; set; }


        /// Profundidad desde el estado inicial hasta este estado.
        public int Profundidad { get; set; }

        /// Costo desde el estado inicial hasta este estado.
        public int CostoG { get; set; }

        /// Heurística estimada desde este estado hasta el objetivo.
        public int HeuristicaH { get; set; }

        /// Costo total (f(n) = g(n) + h(n)).
        public int CostoF => CostoG + HeuristicaH;

        /// Constructor de la clase Estado.
        /// <param name="tablero">Tablero actual.</param>
        /// <param name="padre">Estado padre.</param>
        /// <param name="profundidad">Profundidad desde el inicio.</param>
        /// <param name="costoG">Costo desde el inicio.</param>
        public Estado(Tablero tablero, Estado? padre = null, int profundidad = 0, int costoG = 0)
        {
            Tablero = tablero ?? throw new ArgumentNullException(nameof(tablero), "El tablero no puede ser null.");
            Padre = padre;
            Profundidad = profundidad;
            CostoG = costoG;
            HeuristicaH = Tablero.Heuristica();
        }

        /// Determina si el estado actual es el objetivo (solución).
        /// <returns>True si es el objetivo, de lo contrario, False.</returns>
        public bool EsObjetivo()
        {
            return Tablero.EsSolucionValida();
        }

        /// Genera los estados sucesores desde el estado actual.
        /// <returns>Lista de estados sucesores.</returns>
        public List<Estado> GenerarSucesores()
        {
            var sucesores = new List<Estado>();
            var movimientosPosibles = Tablero.ObtenerMovimientosPosibles();

            foreach (var tableroSucesor in movimientosPosibles)
            {
                // Asumiendo un costo de 1 por movimiento
                var estadoSucesor = new Estado(tableroSucesor, this, Profundidad + 1, CostoG + 1);
                sucesores.Add(estadoSucesor);
            }

            return sucesores;
        }

        /// Calcula la heurística del estado actual.
        /// <returns>Valor de la heurística.</returns>
        public int CalcularHeuristica()
        {
            return Tablero.Heuristica();
        }

        /// Compara este estado con otro basado en el costo total.
        /// <param name="other">Otro estado para comparar.</param>
        /// <returns>Resultado de la comparación.</returns>
        public int CompareTo(Estado? other)
        {
            if (other == null) return 1;
            return this.CostoF.CompareTo(other.CostoF);
        }

        /// Determina si otro estado es igual a este.
        /// <param name="other">Otro estado para comparar.</param>
        /// <returns>True si son iguales, de lo contrario, False.</returns>
        public bool Equals(Estado? other)
        {
            if (other == null)
                return false;

            return Tablero.Equals(other.Tablero);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Estado);
        }

        public override int GetHashCode()
        {
            return Tablero.GetHashCode();
        }

        /// Método para procesar el estado actual (para depuración o análisis).
        public void ProcesarEstado()
        {
            if (Tablero == null)
            {
                Console.WriteLine("El tablero actual es null.");
                return;
            }

            // Verificar si todos los flujos están conectados
            if (Tablero.TodosFlujosConectados())
            {
                Console.WriteLine("Todos los flujos están conectados.");
            }
            else
            {
                Console.WriteLine("No todos los flujos están conectados.");
            }

            // Verificar si es una solución válida
            if (Tablero.EsSolucionValida())
            {
                Console.WriteLine("Es una solución válida.");
            }
            else
            {
                Console.WriteLine("No es una solución válida.");
            }

            // Obtener movimientos posibles
            var movimientos = Tablero.ObtenerMovimientosPosibles();
            Console.WriteLine($"Número de movimientos posibles: {movimientos.Count}");

            // Verificar si tiene bloqueos
            if (Tablero.TieneBloqueos())
            {
                Console.WriteLine("El tablero tiene bloqueos.");
            }
            else
            {
                Console.WriteLine("El tablero no tiene bloqueos.");
            }

            object obj = Tablero.ObtenerMovimientosPosibles(); // Correcto
        }
    }
}
