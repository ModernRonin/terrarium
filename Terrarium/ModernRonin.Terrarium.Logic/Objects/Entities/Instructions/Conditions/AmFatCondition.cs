namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public class AmFatCondition : ACondition
    {
        protected override bool IsFullfilled(IEntityState state, Parameters parameters) =>
            state.StoredEnergy + state.TickEnergy >= parameters.FatThreshold;
    }
}