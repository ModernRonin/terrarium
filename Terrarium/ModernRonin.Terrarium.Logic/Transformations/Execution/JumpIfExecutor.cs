using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class JumpIfExecutor : AnEntityInstructionExecutor<JumpIfInstruction>
    {
        protected override Entity ExecuteOn(JumpIfInstruction instruction, IEntity entity)
        {
            var isConditionTrue = instruction.Condition.IsFulfilledFor(entity);
            var newState = isConditionTrue ? entity.State.WithJump(instruction) : entity.State.NextInstructionIndex();
            return entity.WithState(newState);
        }
    }
}