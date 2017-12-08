using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface IInstructionExecutor
    {
        IEntity Execute(IInstruction currentInstruction, IEntity entity, ISimulationState simulationState);
    }
}