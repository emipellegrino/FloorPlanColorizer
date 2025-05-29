using System;
using FloorPlanColorizer.Interfaces;
using FloorPlanColorizer.Models;
using FloorPlanColorizer.Services;

namespace FloorPlanColorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Cargar desde archivo
                string floorPlanSource = @"FloorPlans/plan1.txt";
                // Alternativa: usar una cadena directamente
                // string floorPlanSource = @"
                // ##########
                // #  ##   #
                // #  # #  #
                // #  ##   #
                // ##########";

                IFloorPlanLoader loader = new FloorPlanLoader();
                IRoomIdentifier identifier = new RoomIdentifier();
                IDoorDetector doorDetector = new DoorDetector();
                IColorizer colorizer = new Colorizer();
                IRenderer renderer = new SpectreConsoleRenderer();

                var floorPlan = new FloorPlan(loader, identifier, doorDetector, colorizer, renderer);
                floorPlan.ProcessFloorPlan(floorPlanSource);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}