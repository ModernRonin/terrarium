using System.Collections.Generic;

namespace ModernRonin.Terrarium.Logic
{
    public static class SimulationStateExtensions
    {
        public static ISimulationState WithEntities(this ISimulationState self, IEnumerable<Entity> entities) =>
            new SimulationState(entities, self.EnergySources, self.Size, self.EnergyDensity);
    }
}