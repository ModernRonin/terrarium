using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using MoreLinq;

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
            var entities = changed.Concat(simulationState.Entities.Except(entity.AsEnumerable()));
            return simulationState.WithEntities(entities);
        }
        protected abstract Entity ExecuteOn(T instruction, IEntity entity);
    }
}