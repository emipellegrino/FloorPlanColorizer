using System;

namespace FloorPlanColorizer.Services
{
    public class RoomIdentifier : Interfaces.IRoomIdentifier
    {
        public int[,] IdentifyRooms(char[,] floorPlan)
        {
            int rows = floorPlan.GetLength(0);
            int cols = floorPlan.GetLength(1);
            int[,] roomMap = new int[rows, cols];
            bool[,] doorMap = new bool[rows, cols]; // Mapa para marcar puertas
            int roomId = 1;

            // Paso 1: Identificar puertas
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (floorPlan[i, j] != ' ') continue;

                    // Puerta horizontal: # a la izquierda y derecha
                    if (j > 0 && j < cols - 1 && floorPlan[i, j - 1] == '#' && floorPlan[i, j + 1] == '#')
                    {
                        doorMap[i, j] = true;
                        Console.WriteLine($"Puerta potencial detectada en ({i},{j}) para separación de habitaciones");
                    }
                    // Puerta vertical: # arriba y abajo
                    else if (i > 0 && i < rows - 1 && floorPlan[i - 1, j] == '#' && floorPlan[i + 1, j] == '#')
                    {
                        doorMap[i, j] = true;
                        Console.WriteLine($"Puerta potencial detectada en ({i},{j}) para separación de habitaciones");
                    }
                }
            }

            // Paso 2: Asignar IDs a habitaciones, excluyendo puertas
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (floorPlan[i, j] == ' ' && roomMap[i, j] == 0 && !doorMap[i, j])
                    {
                        Console.WriteLine($"Asignando ID {roomId} a nueva habitación en ({i},{j})");
                        FloodFill(floorPlan, roomMap, doorMap, i, j, roomId);
                        roomId++;
                    }
                }
            }

            // Diagnóstico: Imprimir roomMap
            Console.WriteLine("Mapa de habitaciones:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{roomMap[i, j],2}");
                }
                Console.WriteLine();
            }

            return roomMap;
        }

        private void FloodFill(char[,] floorPlan, int[,] roomMap, bool[,] doorMap, int row, int col, int roomId)
        {
            if (row < 0 || row >= floorPlan.GetLength(0) || col < 0 || col >= floorPlan.GetLength(1))
                return;
            if (floorPlan[row, col] != ' ' || roomMap[row, col] != 0 || doorMap[row, col])
                return;

            roomMap[row, col] = roomId;

            // Recursión en las cuatro direcciones
            FloodFill(floorPlan, roomMap, doorMap, row - 1, col, roomId); // Arriba
            FloodFill(floorPlan, roomMap, doorMap, row + 1, col, roomId); // Abajo
            FloodFill(floorPlan, roomMap, doorMap, row, col - 1, roomId); // Izquierda
            FloodFill(floorPlan, roomMap, doorMap, row, col + 1, roomId); // Derecha
        }
    }
}