using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class ToggleThrustersExecutor : AnEntityStateInstructionExecutor<ToggleThrustersInstruction>
    {
        protected override IEntityState Transform(IEntityState oldState, ToggleThrustersInstruction instruction) =>
            oldState.AreThrustersOn ? oldState.ThrustOff() : oldState.ThrustOn();
    }
}