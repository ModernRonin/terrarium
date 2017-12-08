using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface IInstructionExecutor
    {
        IEntity Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState);
    }

    public interface ISpecificInstructionExecutor : IInstructionExecutor
    {
        Type HandledInstructionType { get; }
    }

    public abstract class ASpecificInstructionExecutor<T> : ISpecificInstructionExecutor where T:IInstruction
    {
        public IEntity Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState)
        {
            return DoExecute((T) instruction, entity, simulationState);
        }
        public Type HandledInstructionType => typeof(T);
        protected abstract IEntity DoExecute(T instruction, IEntity entity, ISimulationState simulationState);
    }
    public class InstructionExecutor : IInstructionExecutor
    {
        readonly Dictionary<Type, ISpecificInstructionExecutor> mSpecificExecutors;
        public InstructionExecutor(IEnumerable<ISpecificInstructionExecutor> specificExecutors)
        {
            mSpecificExecutors = specificExecutors.ToDictionary(e => e.HandledInstructionType);
        }
        public IEntity Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState)
        {
            if (!mSpecificExecutors.ContainsKey(instruction.GetType()))
                throw new NotImplementedException();
            return mSpecificExecutors[instruction.GetType()].Execute(instruction, entity, simulationState);
        }
    }
}