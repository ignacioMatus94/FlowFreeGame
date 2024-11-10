using System;
using FlowFreeGame;

namespace FlowFreeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Aqui se muestra la interfaz
                Console.WriteLine("Seleccione el tablero a utilizar:");
                Console.WriteLine("1. Tablero 2x2 con 1 Color");
                Console.WriteLine("2. Tablero 3x3 con 1 Color");
                Console.WriteLine("3. Tablero 5x5 con 2 Colores");
                Console.WriteLine("4. Tablero 6x6 con 3 Colores");
                Console.WriteLine("5. Salir");
                Console.Write("Ingrese el número de la opción: ");
                string? opcionTablero = Console.ReadLine();

                if (!int.TryParse(opcionTablero, out int seleccionTablero) || seleccionTablero < 1 || seleccionTablero > 5)
                {
                    Console.WriteLine("Opción inválida. Por favor, ingrese un número entre 1 y 5.\n");
                    continue;
                }

                if (seleccionTablero == 5)
                {
                    Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
                    break;
                }

                //permite seleccionar los tableros
                byte[,]? matrizTablero = seleccionTablero switch
                {
                    1 => TablerosPredefinidos.Tablero2x2_1Color(),
                    2 => TablerosPredefinidos.Tablero3x3_1Color(),
                    3 => TablerosPredefinidos.Tablero5x5_2Colores(),
                    4 => TablerosPredefinidos.Tablero6x6_3Colores(),
                    _ => null
                };

                if (matrizTablero == null)
                {
                    Console.WriteLine("Error al seleccionar el tablero.\n");
                    continue;
                }

                Tablero tableroSeleccionado = new Tablero(matrizTablero);

                Console.WriteLine("\nTablero Seleccionado:");
                tableroSeleccionado.Imprimir();
                Console.WriteLine();

                //Seleccion de algoritmos
                while (true)
                {
                    Console.WriteLine("Seleccione el algoritmo a utilizar:");
                    Console.WriteLine("1. IDS (Iterative Deepening Search)");
                    Console.WriteLine("2. A*");
                    Console.WriteLine("3. IDA*");
                    Console.WriteLine("4. CSP");
                    Console.WriteLine("5. Beam Search");
                    Console.WriteLine("6. Volver al Menú Principal");
                    Console.Write("Ingrese el número de la opción: ");
                    string? opcionAlgoritmo = Console.ReadLine();

                    if (!int.TryParse(opcionAlgoritmo, out int seleccionAlgoritmo) || seleccionAlgoritmo < 1 || seleccionAlgoritmo > 6)
                    {
                        Console.WriteLine("Opción inválida. Por favor, ingrese un número entre 1 y 6.\n");
                        continue;
                    }

                    if (seleccionAlgoritmo == 6)
                    {
                        Console.WriteLine();
                        break;
                    }

                    bool solucionEncontrada = false;
                    int nodosExplorados = 0;
                    double tiempoTotal = 0;

                    //se muestra las estadisticas : se menciona si se encontro:
                    //1) la solucion, 
                    //2) los nodos explorados 
                    //3) tiempo total
                    
                    switch (seleccionAlgoritmo)
                    {
                        case 1:
                            Console.Write("Ingrese la profundidad máxima para IDS: ");
                            string? profundidadInput = Console.ReadLine();
                            if (!int.TryParse(profundidadInput, out int profundidadMax) || profundidadMax < 0)
                            {
                                Console.WriteLine("Profundidad inválida. Debe ser un número entero no negativo.\n");
                                continue;
                            }
                            solucionEncontrada = FlowFreeSolverImplementacion.ResolverIDS(tableroSeleccionado, profundidadMax, out nodosExplorados, out tiempoTotal);
                            Console.WriteLine($"IDS - Nodos explorados: {nodosExplorados}, Tiempo total: {tiempoTotal:F4} segundos.\n");
                            break;
                        case 2:
                            solucionEncontrada = FlowFreeSolverImplementacion.ResolverAStar(tableroSeleccionado, out nodosExplorados, out tiempoTotal);
                            Console.WriteLine($"A* - Nodos explorados: {nodosExplorados}, Tiempo total: {tiempoTotal:F4} segundos.\n");
                            break;
                        case 3:
                            solucionEncontrada = FlowFreeSolverImplementacion.ResolverIDAEstrella(tableroSeleccionado, out nodosExplorados, out tiempoTotal);
                            Console.WriteLine($"IDA* - Nodos explorados: {nodosExplorados}, Tiempo total: {tiempoTotal:F4} segundos.\n");
                            break;
                        case 4:
                            solucionEncontrada = FlowFreeSolverImplementacion.ResolverCSP(tableroSeleccionado, out nodosExplorados, out tiempoTotal);
                            Console.WriteLine($"CSP - Nodos explorados: {nodosExplorados}, Tiempo total: {tiempoTotal:F4} segundos.\n");
                            break;
                        case 5:
                            Console.Write("Ingrese el ancho para Beam Search: ");
                            string? anchoInput = Console.ReadLine();
                            if (!int.TryParse(anchoInput, out int ancho) || ancho <= 0)
                            {
                                Console.WriteLine("Ancho inválido. Debe ser un número entero positivo.\n");
                                continue;
                            }
                            solucionEncontrada = FlowFreeSolverImplementacion.ResolverBeamSearch(tableroSeleccionado, ancho, out nodosExplorados, out tiempoTotal);
                            Console.WriteLine($"Beam Search - Nodos explorados: {nodosExplorados}, Tiempo total: {tiempoTotal:F4} segundos.\n");
                            break;
                    }

                    if (solucionEncontrada)
                    {
                        Console.WriteLine("¡Solución encontrada!\n");
                        tableroSeleccionado.Imprimir();
                    }
                    else
                    {
                        Console.WriteLine("No se encontró una solución.\n");
                    }
                }
            }
        }
    }
}
