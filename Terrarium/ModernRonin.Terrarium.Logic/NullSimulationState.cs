using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic
{
    public class NullSimulationState : ISimulationState
    {
        public Vector2D Size => new Vector2D(100, 100);
        public IEnumerable<IEntity> Entities => Null.Enumerable<Entity>();
        public float[,] EnergyDensity => new float[100, 100];
        public IEnumerable<IEnergySource> EnergySources => Null.Enumerable<IEnergySource>();
        public IEnumerable<IEntity> GetEntitiesAt(Vector2D position) => Null.Enumerable<Entity>();
        public ISimulationState WithEnergySources(IEnumerable<IEnergySource> energySources) => this;
        public ISimulationState WithEntities(IEnumerable<IEntity> entities) => this;
        public float EnergyDensityAt(Vector2D position) => 0;
    }
}