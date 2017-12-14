using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Collision
{
    public interface ICollisionDetection
    {
        ICollisionDetection Excepting(Rectangle2D[] rectanglesOfSizeOne);
        bool IsFreeAt(Vector2D position);
    }
}