using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityCurrentInstructionCostTransformer : AnEntityEnergyCostTransformer
    {
        public EntityCurrentInstructionCostTransformer(IEnergyCostConfiguration configuration) : base(configuration) { }
        protected override float CalculateCost(Entity entity)
        {
            var instructionIndex = entity.State.CurrentInstructionIndex;
            var currentInstruction = entity.Genome.Instructions[instructionIndex];
            return Configuration.GetEnergyCostForInstruction(currentInstruction);
        }
    }
}