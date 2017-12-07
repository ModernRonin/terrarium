using System.Linq;
using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityPartsEnergyCostTransformer : AnEntityEnergyCostTransformer
    {
        public EntityPartsEnergyCostTransformer(IEnergyCostConfiguration configuration) : base(configuration) { }
        protected override float CalculateCost(Entity entity)
        {
            float partCost(Part part) => Configuration.GetEnergyCostForPartKind(part.Kind);

            return entity.State.Parts.Select(partCost).Sum();
        }
    }
}