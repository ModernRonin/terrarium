using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public abstract class AnEntityStateInstructionExecutor<T> : AnEntityInstructionExecutor<T> where T : IInstruction
    {
        protected sealed override Entity Transform(IEntity entity, T instruction)
        {
            var oldState = entity.State;
            var newState = Transform(oldState, instruction);
            return entity.WithState(newState);
        }
        protected abstract IEntityState Transform(IEntityState oldState, T instruction);
    }
}