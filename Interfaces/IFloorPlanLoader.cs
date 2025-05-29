namespace FloorPlanColorizer.Interfaces
{
    public interface IFloorPlanLoader
    {
        char[,] LoadFloorPlan(string source);
    }
}