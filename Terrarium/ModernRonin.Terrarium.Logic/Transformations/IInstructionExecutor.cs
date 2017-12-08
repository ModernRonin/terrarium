using System;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface IInstructionExecutor 
    {
        Type HandledInstructionType { get; }
        ISimulationState Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState);
    }

    public abstract class AInstructionExecutor<T> : IInstructionExecutor where T : IInstruction
    {
        public ISimulationState Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState) =>
            DoExecute((T) instruction, entity, simulationState);
        public Type HandledInstructionType => typeof(T);
        protected abstract ISimulationState DoExecute(T instruction, IEntity entity, ISimulationState simulationState);
    }
}