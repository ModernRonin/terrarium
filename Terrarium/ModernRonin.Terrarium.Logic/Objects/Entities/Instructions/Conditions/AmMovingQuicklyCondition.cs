namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public class AmMovingQuicklyCondition : ACondition
    {
        protected override bool IsFullfilled(IEntityState state, Parameters parameters) =>
            state.LastDistanceMovedSquared >= parameters.MovingQuicklyThreshold;
    }
}