namespace FloorPlanColorizer.Interfaces
{
    public interface IRoomIdentifier
    {
        int[,] IdentifyRooms(char[,] floorPlan);
    }
}