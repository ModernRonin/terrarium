using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public abstract class AnEntityInstructionExecutor<T> : AnInstructionExecutor<T> where T : IInstruction
    {
        protected sealed override ISimulationState DoExecute(
            T instruction,
            IEntity entity,
            ISimulationState simulationState)
        {
            var changed = ExecuteOn(instruction, entity);
            return simulationState.ReplaceEntity(entity, changed);
        }
        protected abstract Entity ExecuteOn(T instruction, IEntity entity);
    }
}