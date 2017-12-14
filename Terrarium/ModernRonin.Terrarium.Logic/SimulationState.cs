using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Collision;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic
{
    public class SimulationState : ISimulationState
    {
        public SimulationState(
            IEnumerable<IEntity> entities,
            IEnumerable<IEnergySource>energySources,
            Vector2D size = new Vector2D(),
            ICollisionDetection collisionDetection = null,
            float[,] energyDensity = null)
        {
            Size = size;
            CollisionDetection = collisionDetection;
            Entities = entities;
            EnergySources = energySources;
            EnergyDensity = energyDensity ?? EnergySources.Aggregate(Size.ToEnergyDensity(), (g, s) => s.ApplyTo(g));
        }
        public ICollisionDetection CollisionDetection { get; }
        public Vector2D Size { get; }
        public IEnumerable<IEntity> Entities { get; }
        public float[,] EnergyDensity { get; }
        public IEnumerable<IEnergySource> EnergySources { get; }
        public IEnumerable<IEntity> GetEntitiesAt(Vector2D position)
        {
            return Entities.Where(e => e.State.AbsoluteBoundingBox.Contains(position));
        }
        public ISimulationState WithEnergySources(IEnumerable<IEnergySource> energySources) =>
            new SimulationState(Entities, energySources, Size, CollisionDetection);
        public ISimulationState WithEntities(IEnumerable<IEntity> entities) =>
            new SimulationState(entities, EnergySources, Size, CollisionDetection, EnergyDensity);
        public float EnergyDensityAt(Vector2D position)
        {
            var x = (int) position.X;
            var y = (int) position.Y;
            return EnergyDensity[x, y];
        }
        public ISimulationState WithCollisionDetection(ICollisionDetection collisionDetection) =>
            new SimulationState(Entities, EnergySources, Size, collisionDetection, EnergyDensity);
    }
}