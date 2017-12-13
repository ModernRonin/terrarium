using System;

namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public class AmMovingCondition : ACondition
    {
        protected override bool IsFullfilled(IEntityState state, Parameters parameters) =>
            state.LastDistanceMovedSquared > 0.1f;
    }
}