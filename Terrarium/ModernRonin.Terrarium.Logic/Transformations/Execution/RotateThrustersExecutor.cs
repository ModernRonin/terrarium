using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class RotateThrustersExecutor : AnEntityStateInstructionExecutor<RotateThrustersInstruction>
    {
        protected override IEntityState Transform(IEntityState oldState, RotateThrustersInstruction instruction) =>
            oldState.WithThrustDirection(instruction.NewRotation);
    }
}