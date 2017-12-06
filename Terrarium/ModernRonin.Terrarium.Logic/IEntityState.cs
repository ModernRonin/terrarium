using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic {
    public interface IEntityState {
        Vector2D Position { get; }
        IEnumerable<Part> Parts { get; }
        string Code { get; }
        Rectangle2D LocalBoundingBox { get; }
        Rectangle2D AbsoluteBoundingBox { get; }
        EntityState At(Vector2D position);
    }
}