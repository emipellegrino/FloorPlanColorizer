using FloorPlanColorizer.Interfaces;
using System;
using System.Linq;

namespace FloorPlanColorizer.Models
{
    public class FloorPlan
    {
        private readonly IFloorPlanLoader _loader;
        private readonly IRoomIdentifier _roomIdentifier;
        private readonly IDoorDetector _doorDetector;
        private readonly IColorizer _colorizer;
        private readonly IRenderer _renderer;

        public FloorPlan(IFloorPlanLoader loader, IRoomIdentifier roomIdentifier, IDoorDetector doorDetector, IColorizer colorizer, IRenderer renderer)
        {
            _loader = loader;
            _roomIdentifier = roomIdentifier;
            _doorDetector = doorDetector;
            _colorizer = colorizer;
            _renderer = renderer;
        }

        public void ProcessFloorPlan(string source)
        {
            var plan = _loader.LoadFloorPlan(source);
            var roomMap = _roomIdentifier.IdentifyRooms(plan);
            var doorMap = _doorDetector.DetectDoors(plan, roomMap, out var roomsWithDoors);

            var allRoomIds = roomMap.Cast<int>().Where(id => id > 0).Distinct().ToList();
            var missingDoors = allRoomIds.Except(roomsWithDoors).ToList();
            if (missingDoors.Any())
            {
                throw new InvalidOperationException($"Las siguientes habitaciones no tienen puertas: {string.Join(", ", missingDoors)}");
            }

            var coloredPlan = _colorizer.Colorize(roomMap, plan, doorMap);
            _renderer.Render(coloredPlan);
        }
    }
}