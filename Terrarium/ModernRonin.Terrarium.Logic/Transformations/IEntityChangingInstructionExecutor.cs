using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface IEntityChangingInstructionExecutor
    {
        IEntity Execute(IEntityChangingInstruction instruction, IEntity entity, ISimulationState simulationState);
    }
}