using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class ToggleThrustersExecutor : AnEntityInstructionExecutor<ToggleThrustersInstruction>
    {
        protected override Entity Transform(IEntity entity)
        {
            var changed = entity.State.AreThrustersOn
                ? entity.WithState(entity.State.ThrustOff())
                : entity.WithState(entity.State.ThrustOn());
            return changed;
        }
    }
}