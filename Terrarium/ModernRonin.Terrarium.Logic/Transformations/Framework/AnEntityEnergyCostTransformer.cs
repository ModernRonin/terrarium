using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations.Framework
{
    public abstract class AnEntityEnergyCostTransformer : AnEntityEnergyTransformer
    {
        public AnEntityEnergyCostTransformer(IEnergyCostConfiguration configuration) => Configuration = configuration;
        public IEnergyCostConfiguration Configuration { get; }
        protected sealed override IEntity Transform(IEntity old, ISimulationState state)
        {
            var cost = CalculateCost(old);
            return Subtract(old, cost);
        }
        protected abstract float CalculateCost(IEntity entity);
    }
}