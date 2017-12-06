using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct SimulationState : ISimulationState
    {
        public SimulationState(
            IEnumerable<Entity> entities,
            IEnumerable<EnergySource>energySources,
            Vector2D size = new Vector2D(),
            float[,] energyDensity = null)
        {
            Size = size;
            Entities = entities;
            EnergySources = energySources;
            EnergyDensity = energyDensity ?? EnergySources.Aggregate(Size.ToEnergyDensity(), (g, s) => s.ApplyTo(g));
        }
        public Vector2D Size { get; }
        public IEnumerable<Entity> Entities { get; }
        public float[,] EnergyDensity { get; }
        public IEnumerable<EnergySource> EnergySources { get; }
        public IEnumerable<Entity> GetEntitiesAt(Vector2D position)
        {
            return Entities.Where(e => e.State.AbsoluteBoundingBox.Contains(position));
        }
    }
}