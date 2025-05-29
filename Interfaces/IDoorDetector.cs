namespace FloorPlanColorizer.Interfaces
{
    public interface IDoorDetector
    {
        bool[,] DetectDoors(char[,] floorPlan, int[,] roomMap, out HashSet<int> roomsWithDoors);
    }
}