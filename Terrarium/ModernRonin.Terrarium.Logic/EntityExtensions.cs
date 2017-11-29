using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public static class EntityExtensions
    {
        public static Entity At(this Entity self, Vector2D position) => new Entity(self.Parts, position);
    }
}