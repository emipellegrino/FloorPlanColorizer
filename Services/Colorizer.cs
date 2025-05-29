namespace FloorPlanColorizer.Services
{
    public class Colorizer : Interfaces.IColorizer
    {
        private readonly char[] _roomChars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        public char[,] Colorize(int[,] roomMap, char[,] originalPlan, bool[,] doorMap)
        {
            int rows = roomMap.GetLength(0);
            int cols = roomMap.GetLength(1);
            char[,] coloredPlan = (char[,])originalPlan.Clone();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (originalPlan[i, j] == '#')
                    {
                        coloredPlan[i, j] = '#';
                    }
                    else if (doorMap[i, j])
                    {
                        coloredPlan[i, j] = ' ';
                    }
                    else if (roomMap[i, j] > 0)
                    {
                        coloredPlan[i, j] = _roomChars[(roomMap[i, j] - 1) % _roomChars.Length];
                    }
                }
            }
            return coloredPlan;
        }
    }
}