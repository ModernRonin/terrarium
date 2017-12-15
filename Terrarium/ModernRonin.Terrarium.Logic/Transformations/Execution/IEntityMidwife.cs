using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public interface IEntityMidwife
    {
        IEntity GiveBirth(IEntity parent, Vector2D targetPosition);
    }
}