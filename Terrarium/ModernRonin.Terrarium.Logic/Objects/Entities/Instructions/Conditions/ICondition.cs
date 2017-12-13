namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public interface ICondition
    {
        bool IsFulfilledFor(IEntity entity);
    }
}