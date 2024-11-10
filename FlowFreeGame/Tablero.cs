// Tablero.cs
using System;
using System.Collections.Generic;

namespace FlowFreeGame
{
    /// Representa el tablero del juego Flow Free.
    public class Tablero : IEquatable<Tablero>
    {
        /// Matriz que representa el tablero.
        /// Cada número representa un color; 0 puede representar una celda vacía.
        public byte[,] Matriz { get; set; }

        /// Tamaño del tablero (asumido cuadrado para simplificar).
        public int Tamaño { get; set; }

        /// Constructor de la clase Tablero.
        /// <param name="matriz">Matriz que representa el tablero.</param>
        public Tablero(byte[,] matriz)
        {
            Matriz = matriz ?? throw new ArgumentNullException(nameof(matriz), "La matriz no puede ser null.");
            Tamaño = matriz.GetLength(0);
            if (matriz.GetLength(1) != Tamaño)
                throw new ArgumentException("La matriz debe ser cuadrada.");
        }

        /// Determina si el tablero actual es una solución válida.
        /// <returns>True si es una solución válida, de lo contrario, False.</returns>
        public bool EsSolucionValida()
        {
            return !TieneBloqueos() && TodosFlujosConectados();
        }

        /// Verifica si todos los flujos están conectados correctamente.
        /// <returns>True si todos los flujos están conectados, de lo contrario, False.</returns>
        public bool TodosFlujosConectados()
        {
            // Diccionario para almacenar los puntos de cada color
            Dictionary<byte, List<(int, int)>> puntosPorColor = new Dictionary<byte, List<(int, int)>>();

            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    byte color = Matriz[i, j];
                    if (color != 0)
                    {
                        if (!puntosPorColor.ContainsKey(color))
                        {
                            puntosPorColor[color] = new List<(int, int)>();
                        }
                        puntosPorColor[color].Add((i, j));
                    }
                }
            }

            foreach (var color in puntosPorColor.Keys)
            {
                var puntos = puntosPorColor[color];
                if (puntos.Count != 2)
                {
                    // Cada color debe tener exactamente dos puntos
                    return false;
                }

                var inicio = puntos[0];
                var fin = puntos[1];

                if (!ExisteCamino(inicio, fin, color))
                {
                    return false;
                }
            }

            return true;
        }

        /// Verifica si existe un camino válido entre dos puntos de un color específico.
        /// <param name="inicio">Coordenadas de inicio.</param>
        /// <param name="fin">Coordenadas de fin.</param>
        /// <param name="color">Color a verificar.</param>
        /// <returns>True si existe un camino válido, de lo contrario, False.</returns>
        private bool ExisteCamino((int, int) inicio, (int, int) fin, byte color)
        {
            int filas = Tamaño;
            int columnas = Tamaño;
            bool[,] visitado = new bool[filas, columnas];
            Queue<(int, int)> cola = new Queue<(int, int)>();
            cola.Enqueue(inicio);
            visitado[inicio.Item1, inicio.Item2] = true;

            int[] dirFilas = { -1, 1, 0, 0 };
            int[] dirColumnas = { 0, 0, -1, 1 };

            while (cola.Count > 0)
            {
                var actual = cola.Dequeue();
                if (actual == fin)
                    return true;

                for (int d = 0; d < 4; d++)
                {
                    int nuevaFila = actual.Item1 + dirFilas[d];
                    int nuevaColumna = actual.Item2 + dirColumnas[d];

                    if (nuevaFila >= 0 && nuevaFila < filas && nuevaColumna >= 0 && nuevaColumna < columnas &&
                        !visitado[nuevaFila, nuevaColumna] &&
                        (Matriz[nuevaFila, nuevaColumna] == 0 || Matriz[nuevaFila, nuevaColumna] == color))
                    {
                        cola.Enqueue((nuevaFila, nuevaColumna));
                        visitado[nuevaFila, nuevaColumna] = true;
                    }
                }
            }

            return false;
        }

        /// Verifica si el tablero tiene bloqueos que impiden avanzar.
        /// <returns>True si hay bloqueos, de lo contrario, False.</returns>
        public bool TieneBloqueos()
        {
            // Implementación simplificada: Verificar si hay celdas vacías completamente rodeadas por flujos
            // Esta lógica puede necesitar mejoras según las reglas específicas de Flow Free

            bool[,] visitado = new bool[Tamaño, Tamaño];
            Queue<(int, int)> cola = new Queue<(int, int)>();

            // Encolar todas las celdas vacías en los bordes
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    if ((i == 0 || i == Tamaño - 1 || j == 0 || j == Tamaño - 1) && Matriz[i, j] == 0)
                    {
                        cola.Enqueue((i, j));
                        visitado[i, j] = true;
                    }
                }
            }

            int[] dirFilas = { -1, 1, 0, 0 };
            int[] dirColumnas = { 0, 0, -1, 1 };

            while (cola.Count > 0)
            {
                var actual = cola.Dequeue();

                for (int d = 0; d < 4; d++)
                {
                    int nuevaFila = actual.Item1 + dirFilas[d];
                    int nuevaColumna = actual.Item2 + dirColumnas[d];

                    if (nuevaFila >= 0 && nuevaFila < Tamaño && nuevaColumna >= 0 && nuevaColumna < Tamaño &&
                        !visitado[nuevaFila, nuevaColumna] &&
                        Matriz[nuevaFila, nuevaColumna] == 0)
                    {
                        cola.Enqueue((nuevaFila, nuevaColumna));
                        visitado[nuevaFila, nuevaColumna] = true;
                    }
                }
            }

            // Verificar si hay celdas vacías que no han sido visitadas (bloqueadas)
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    if (Matriz[i, j] == 0 && !visitado[i, j])
                        return true;
                }
            }

            return false;
        }

        /// Obtiene los movimientos posibles desde el tablero actual.
        /// <returns>Lista de tableros sucesores.</returns>
        public List<Tablero> ObtenerMovimientosPosibles()
        {
            var movimientos = new List<Tablero>();
            var puntosPorColor = new Dictionary<byte, List<(int, int)>>();

            // Identificar todos los puntos por color
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    byte color = Matriz[i, j];
                    if (color != 0)
                    {
                        if (!puntosPorColor.ContainsKey(color))
                            puntosPorColor[color] = new List<(int, int)>();
                        puntosPorColor[color].Add((i, j));
                    }
                }
            }

            // Generar movimientos para cada color
            foreach (var color in puntosPorColor.Keys)
            {
                var puntos = puntosPorColor[color];
                if (puntos.Count != 2)
                    continue; // Ignorar colores incompletos

                var inicio = puntos[0];
                var fin = puntos[1];

                // Expandir en las cuatro direcciones desde el inicio
                int[] dirFilas = { -1, 1, 0, 0 };
                int[] dirColumnas = { 0, 0, -1, 1 };

                foreach (var d in Enumerable.Range(0, 4))
                {
                    int nuevaFila = inicio.Item1 + dirFilas[d];
                    int nuevaColumna = inicio.Item2 + dirColumnas[d];

                    if (nuevaFila >= 0 && nuevaFila < Tamaño && nuevaColumna >= 0 && nuevaColumna < Tamaño &&
                        Matriz[nuevaFila, nuevaColumna] == 0)
                    {
                        // Crear una copia de la matriz y realizar el movimiento
                        byte[,] nuevaMatriz = (byte[,])Matriz.Clone();
                        nuevaMatriz[nuevaFila, nuevaColumna] = color;

                        movimientos.Add(new Tablero(nuevaMatriz));
                    }
                }
            }

            return movimientos;
        }

        /// Calcula la heurística del tablero actual.
        /// Por ejemplo, la suma de las distancias Manhattan de cada par de puntos.
        /// <returns>Valor de la heurística.</returns>
        public int Heuristica()
        {
            int heuristica = 0;
            var puntosPorColor = new Dictionary<byte, List<(int, int)>>();

            // Identificar todos los puntos por color
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    byte color = Matriz[i, j];
                    if (color != 0)
                    {
                        if (!puntosPorColor.ContainsKey(color))
                            puntosPorColor[color] = new List<(int, int)>();
                        puntosPorColor[color].Add((i, j));
                    }
                }
            }

            // Calcular la distancia Manhattan para cada par de puntos
            foreach (var color in puntosPorColor.Keys)
            {
                var puntos = puntosPorColor[color];
                if (puntos.Count != 2)
                    continue; // Ignorar colores incompletos

                var inicio = puntos[0];
                var fin = puntos[1];

                heuristica += Math.Abs(inicio.Item1 - fin.Item1) + Math.Abs(inicio.Item2 - fin.Item2);
            }

            return heuristica;
        }

        public bool Equals(Tablero? other)
        {
            if (other == null)
                return false;

            if (Tamaño != other.Tamaño)
                return false;

            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    if (Matriz[i, j] != other.Matriz[i, j])
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Tablero);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    hash = hash * 23 + Matriz[i, j].GetHashCode();
                }
            }
            return hash;
        }

        /// Método para imprimir el tablero en la consola.
        public void Imprimir()
        {
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    Console.Write(Matriz[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// Crea una instancia de Tablero desde una matriz dada.
        /// <param name="matriz">Matriz que representa el tablero.</param>
        /// <returns>Instancia de Tablero.</returns>
        public static Tablero CrearDesdeMatriz(byte[,] matriz)
        {
            return new Tablero(matriz);
        }
    }
}
