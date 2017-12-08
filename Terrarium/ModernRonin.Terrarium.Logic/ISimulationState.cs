using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulationState
    {
        Vector2D Size { get; }
        IEnumerable<IEntity> Entities { get; }
        float[,] EnergyDensity { get; }
        IEnumerable<IEnergySource> EnergySources { get; }
        IEnumerable<IEntity> GetEntitiesAt(Vector2D position);
        ISimulationState WithEnergySources(IEnumerable<IEnergySource> energySources);
        ISimulationState WithEntities(IEnumerable<IEntity> entities);
        float EnergyDensityAt(Vector2D position);
    }
}