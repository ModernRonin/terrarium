using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulationState
    {
        Vector2D Size { get; }
        IEnumerable<EntityState> Entities { get; }
        float[,] EnergyDensity { get; }
        IEnumerable<EnergySource> EnergySources { get; }
        IEnumerable<EntityState> GetEntitiesAt(Vector2D position);
    }
}