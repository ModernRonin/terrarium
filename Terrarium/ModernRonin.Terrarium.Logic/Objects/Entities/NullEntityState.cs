using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class NullEntityState : IEntityState
    {
        public Vector2D Position => Vector2D.Zero;
        public IEnumerable<Part> Parts => Null.Enumerable<Part>();
        public string Code => string.Empty;
        public Rectangle2D LocalBoundingBox => new Rectangle2D(Vector2D.Zero, Vector2D.Zero);
        public Rectangle2D AbsoluteBoundingBox => new Rectangle2D(Vector2D.Zero, Vector2D.Zero);
        public float TickEnergy => 0;
        public int CurrentInstructionIndex => 0;
        public IEntityState At(Vector2D position) => this;
        public IEntityState WithParts(IEnumerable<Part> parts) => this;
        public IEntityState AddTickEnergy(float delta) => this;
        public IEntityState SubtractTickEnergy(float delta) => this;
        public IEntityState ResetTickEnergy() => this;
        public IEntityState WithCurrentInstructionIndex(int index) => this;
    }
}