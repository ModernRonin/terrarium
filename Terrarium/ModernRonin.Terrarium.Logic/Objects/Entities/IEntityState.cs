using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public interface IEntityState {
        Vector2D Position { get; }
        IEnumerable<Part> Parts { get; }
        string Code { get; }
        Rectangle2D LocalBoundingBox { get; }
        Rectangle2D AbsoluteBoundingBox { get; }
        float TickEnergy { get; }
        IEntityState At(Vector2D position);
        IEntityState WithParts(IEnumerable<Part> parts);
        IEntityState AddTickEnergy(float delta);
        IEntityState SubtractTickEnergy(float delta);
    }
}