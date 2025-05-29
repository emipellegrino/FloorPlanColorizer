using System;
using System.IO;

namespace FloorPlanColorizer.Services
{
    public class FloorPlanLoader : Interfaces.IFloorPlanLoader
    {
        public char[,] LoadFloorPlan(string source)
        {
            string[] lines;
            // Construir la ruta completa relativa al directorio de ejecución
            string filePath = Path.Combine(AppContext.BaseDirectory, source);

            // Diagnóstico: Imprimir la ruta que se está intentando leer
            Console.WriteLine($"Intentando cargar archivo desde: {filePath}");

            if (File.Exists(filePath))
            {
                try
                {
                    lines = File.ReadAllLines(filePath);
                }
                catch (IOException ex)
                {
                    throw new IOException($"Error al leer el archivo '{source}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Archivo '{filePath}' no encontrado. Procesando como cadena.");
                // Procesar como cadena si no es un archivo
                lines = source.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            }

            if (lines.Length == 0)
                throw new ArgumentException("El plano está vacío.");

            int rows = lines.Length;
            int cols = lines[0].Length;
            var plan = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                if (lines[i].Length != cols)
                    throw new ArgumentException($"La fila {i + 1} tiene una longitud diferente. Todas las filas deben tener {cols} caracteres.");
                for (int j = 0; j < cols; j++)
                {
                    if (lines[i][j] != '#' && lines[i][j] != ' ')
                        throw new ArgumentException($"Carácter inválido '{lines[i][j]}' en la fila {i + 1}, columna {j + 1}. Solo se permiten '#' y ' '.");
                    plan[i, j] = lines[i][j];
                }
            }
            return plan;
        }
    }
}