using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class RotateThrustersExecutor : AnEntityStateInstructionExecutor<RotateThrustersInstruction>
    {
        protected override bool DoAutoIncrementInstructionPointer => true;
        protected override IEntityState ExecuteOn(RotateThrustersInstruction instruction, IEntityState oldState) =>
            oldState.WithThrustDirection(instruction.NewRotation);
    }
}