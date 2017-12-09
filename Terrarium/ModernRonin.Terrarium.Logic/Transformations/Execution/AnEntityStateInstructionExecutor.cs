using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public abstract class AnEntityStateInstructionExecutor<T> : AnEntityInstructionExecutor<T> where T : IInstruction
    {
        protected sealed override Entity ExecuteOn(T instruction, IEntity entity)
        {
            var oldState = entity.State;
            var newState = ExecuteOn(instruction, oldState);
            if (DoAutoIncrementInstructionPointer)
                newState=newState.WithCurrentInstructionIndex(newState.CurrentInstructionIndex+1);
            return entity.WithState(newState);
        }
        protected abstract IEntityState ExecuteOn(T instruction, IEntityState oldState);
        protected abstract bool DoAutoIncrementInstructionPointer { get; }

    }
}