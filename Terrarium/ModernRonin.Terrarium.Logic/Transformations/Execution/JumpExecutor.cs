using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class JumpExecutor : AnEntityStateInstructionExecutor<JumpInstruction>
    {
        protected override bool DoAutoIncrementInstructionPointer => false;
        protected override IEntityState ExecuteOn(JumpInstruction instruction, IEntityState oldState) =>
            oldState.WithCurrentInstructionIndex(oldState.CurrentInstructionIndex +
                                                 instruction.InstructionPointerDelta);
    }
}