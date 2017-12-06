using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic
{
    public struct SimulationState : ISimulationState
    {
        public SimulationState(
            IEnumerable<Entity> entities,
            IEnumerable<IEnergySource>energySources,
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
        public IEnumerable<IEnergySource> EnergySources { get; }
        public IEnumerable<Entity> GetEntitiesAt(Vector2D position)
        {
            return Entities.Where(e => e.State.AbsoluteBoundingBox.Contains(position));
        }
        public ISimulationState WithEnergySources(IEnumerable<IEnergySource> energySources) =>
            new SimulationState(Entities, energySources, Size);
        public ISimulationState WithEntities(IEnumerable<Entity> entities) =>
            new SimulationState(entities, EnergySources, Size, EnergyDensity);
        public float EnergyDensityAt(Vector2D position)
        {
            var x = (int) position.X;
            var y = (int) position.Y;
            return EnergyDensity[x, y];
        }
    }
}