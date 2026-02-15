using System;
using System.Diagnostics; // Necesario para medir el tiempo (Stopwatch)

namespace MultiplicacionMatrizHPC
{
    class Program
    {
        // El punto de entrada recibe 'args', que es la "mochila" con los datos de la consola
        static void Main(string[] args)
        {
            // --- 1. VALIDACIÓN DE ENTRADA DINÁMICA ---
            // Verificamos si el usuario escribió el tamaño de la matriz en la consola
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Debes indicar el tamaño de la matriz. Ejemplo: dotnet run 1000");
                return;
            }

            // Convertimos el texto de la consola (args[0]) a un número entero 'n'
            // Esto es "dinámico" porque 'n' no tiene un valor fijo en el código
            if (!int.TryParse(args[0], out int n))
            {
                Console.WriteLine("Error: El parámetro debe ser un número entero válido.");
                return;
            }

            Console.WriteLine($"--- Ejecución Secuencial: Matriz de {n}x{n} ---");

            // --- 2. RESERVA DE MEMORIA (MATRICES DINÁMICAS) ---
            // 'int' ocupa 32 bits (4 bytes). Al hacer 'new int[n, n]', 
            // le pedimos a la RAM un bloque de memoria proporcional a 'n * n'.
            int[,] matrizA = new int[n, n];
            int[,] matrizB = new int[n, n];
            int[,] matrizResultado = new int[n, n];

            // --- 3. LLENADO ALEATORIO ---
            Random generadorRnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Llenamos con números entre 1 y 9 para no desbordar el entero rápido
                    matrizA[i, j] = generadorRnd.Next(1, 10);
                    matrizB[i, j] = generadorRnd.Next(1, 10);
                }
            }
            Console.WriteLine("Matrices inicializadas con datos aleatorios.");

            // --- 4. EL NÚCLEO DEL CÁLCULO (ALGORITMO SECUENCIAL) ---
            // Usamos Stopwatch para medir cuánto tarda exactamente la CPU
            Stopwatch reloj = new Stopwatch();
            reloj.Start();

            // Triple bucle for: El estándar O(n^3)
            // Es SECUENCIAL porque un 'for' no empieza hasta que el anterior termina su ciclo.
            for (int i = 0; i < n; i++) // Recorre cada FILA de la matriz A
            {
                for (int j = 0; j < n; j++) // Recorre cada COLUMNA de la matriz B
                {
                    int sumaAcumulada = 0;
                    for (int k = 0; k < n; k++) // Producto punto de fila A por columna B
                    {
                        // Aquí ocurre la computación pura
                        sumaAcumulada += matrizA[i, k] * matrizB[k, j];
                    }
                    matrizResultado[i, j] = sumaAcumulada;
                }
            }

            reloj.Stop(); // Detenemos el tiempo justo al terminar los bucles

            // // --- BLOQUE DE VERIFICACIÓN MANUAL (Solo para N pequeño) ---
            // if (n <= 5) 
            // {
            //     Console.WriteLine("\n--- VERIFICACIÓN MANUAL (N <= 5) ---");

            //     // Mostrar Matriz A
            //     Console.WriteLine("\nMatriz A:");
            //     for (int i = 0; i < n; i++) {
            //         for (int j = 0; j < n; j++) {
            //             Console.Write(matrizA[i, j] + "\t");
            //         }
            //         Console.WriteLine();
            //     }

            //     // Mostrar Matriz B
            //     Console.WriteLine("\nMatriz B:");
            //     for (int i = 0; i < n; i++) {
            //         for (int j = 0; j < n; j++) {
            //             Console.Write(matrizB[i, j] + "\t");
            //         }
            //         Console.WriteLine();
            //     }

            //     // Mostrar Resultado
            //     Console.WriteLine("\nMatriz Resultado (A x B):");
            //     for (int i = 0; i < n; i++) {
            //         for (int j = 0; j < n; j++) {
            //             Console.Write(matrizResultado[i, j] + "\t");
            //         }
            //         Console.WriteLine();
            //     }
            //     Console.WriteLine("------------------------------------\n");
            // }

            // --- 5. RESULTADOS ---
            Console.WriteLine("Cálculo finalizado con éxito.");
            // Mostramos el tiempo con 4 decimales para notar la diferencia en pruebas pequeñas
            Console.WriteLine($"Tiempo total de ejecución: {reloj.Elapsed.TotalSeconds:F4} segundos.");
            
            // Nota: No imprimimos la matriz de 1000x1000 porque saturaría la consola.
        }
    }
}
