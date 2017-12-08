using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface IInstructionExecutor
    {
        IEntity Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState);
    }
}