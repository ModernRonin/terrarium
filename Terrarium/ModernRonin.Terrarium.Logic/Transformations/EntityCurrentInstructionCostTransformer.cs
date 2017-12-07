using ModernRonin.Terrarium.Logic.Objects.Entities;

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