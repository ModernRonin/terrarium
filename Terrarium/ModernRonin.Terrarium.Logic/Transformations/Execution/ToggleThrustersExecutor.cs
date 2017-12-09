using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class ToggleThrustersExecutor : AnEntityStateInstructionExecutor<ToggleThrustersInstruction>
    {
        protected override IEntityState ExecuteOn(ToggleThrustersInstruction instruction, IEntityState oldState) =>
            oldState.AreThrustersOn ? oldState.ThrustOff() : oldState.ThrustOn();
        protected override bool DoAutoIncrementInstructionPointer => true;
    }
}