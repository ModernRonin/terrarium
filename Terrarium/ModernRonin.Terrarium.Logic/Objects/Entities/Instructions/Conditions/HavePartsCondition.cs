using System;

namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public class HavePartsCondition : ACondition
    {
        protected override bool IsFullfilled(IEntityState state, Parameters parameters) =>
            throw new NotImplementedException();
    }
}