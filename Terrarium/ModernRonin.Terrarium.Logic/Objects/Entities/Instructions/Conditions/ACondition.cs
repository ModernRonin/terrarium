namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public abstract class ACondition : ICondition
    {
        public bool IsFulfilledFor(IEntity entity)
        {
            var parameters = entity.Genome.Parameters;
            var state = entity.State;
            return IsFullfilled(state, parameters);
        }
        protected abstract bool IsFullfilled(IEntityState state, Parameters parameters);
    }
}