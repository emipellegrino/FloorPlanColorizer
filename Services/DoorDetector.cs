using System.Collections.Generic;

namespace FloorPlanColorizer.Services
{
    public class DoorDetector : Interfaces.IDoorDetector
    {
        public bool[,] DetectDoors(char[,] floorPlan, int[,] roomMap, out HashSet<int> roomsWithDoors)
        {
            int rows = floorPlan.GetLength(0);
            int cols = floorPlan.GetLength(1);
            bool[,] doorMap = new bool[rows, cols];
            roomsWithDoors = new HashSet<int>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (floorPlan[i, j] != ' ') continue;

                    // Puerta horizontal: # a la izquierda y derecha
                    if (j > 0 && j < cols - 1 && floorPlan[i, j - 1] == '#' && floorPlan[i, j + 1] == '#')
                    {
                        int roomAbove = i > 0 ? roomMap[i - 1, j] : 0;
                        int roomBelow = i < rows - 1 ? roomMap[i + 1, j] : 0;

                        if (roomAbove > 0 && roomBelow > 0 && roomAbove != roomBelow)
                        {
                            doorMap[i, j] = true;
                            roomsWithDoors.Add(roomAbove);
                            roomsWithDoors.Add(roomBelow);
                            System.Console.WriteLine($"Puerta horizontal detectada en ({i},{j}): Conecta {roomAbove} y {roomBelow}");
                        }
                    }
                    // Puerta vertical: # arriba y abajo
                    else if (i > 0 && i < rows - 1 && floorPlan[i - 1, j] == '#' && floorPlan[i + 1, j] == '#')
                    {
                        int roomLeft = j > 0 ? roomMap[i, j - 1] : 0;
                        int roomRight = j < cols - 1 ? roomMap[i, j + 1] : 0;

                        if (roomLeft > 0 && roomRight > 0 && roomLeft != roomRight)
                        {
                            doorMap[i, j] = true;
                            roomsWithDoors.Add(roomLeft);
                            roomsWithDoors.Add(roomRight);
                            System.Console.WriteLine($"Puerta vertical detectada en ({i},{j}): Conecta {roomLeft} y {roomRight}");
                        }
                    }
                }
            }
            return doorMap;
        }
    }
}