using System;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution {
    public interface IInstructionExecutor 
    {
        Type HandledInstructionType { get; }
        ISimulationState Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState);
    }
}