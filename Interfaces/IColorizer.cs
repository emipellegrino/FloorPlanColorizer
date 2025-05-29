namespace FloorPlanColorizer.Interfaces
{
    public interface IColorizer
    {
        char[,] Colorize(int[,] roomMap, char[,] originalPlan, bool[,] doorMap);
    }
}
