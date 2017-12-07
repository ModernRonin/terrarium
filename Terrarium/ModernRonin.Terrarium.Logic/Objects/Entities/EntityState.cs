using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class EntityState : IEntityState
    {
        public EntityState(
            IEnumerable<Part> parts,
            Vector2D position = new Vector2D(),
            float tickEnergy = 0,
            int currentInstructionIndex = 0)
        {
            Position = position;
            TickEnergy = tickEnergy;
            CurrentInstructionIndex = currentInstructionIndex;
            Parts = parts;
        }
        public Vector2D Position { get; }
        public IEnumerable<Part> Parts { get; }
        public float TickEnergy { get; }
        public int CurrentInstructionIndex { get; }
        public string Code => string.Join("?", Parts.Select(p => p.Code));
        public Rectangle2D LocalBoundingBox
        {
            get
            {
                var minX = Parts.Min(p => p.RelativePosition.X);
                var maxX = Parts.Max(p => p.RelativePosition.X) + 1;
                var minY = Parts.Min(p => p.RelativePosition.Y);
                var maxY = Parts.Max(p => p.RelativePosition.Y) + 1;
                return new Rectangle2D(new Vector2D(minX, minY), new Vector2D(maxX, maxY));
            }
        }
        public Rectangle2D AbsoluteBoundingBox => LocalBoundingBox.RelativeTo(Position);
        public IEntityState At(Vector2D position) => new EntityState(Parts, position, TickEnergy, CurrentInstructionIndex);
        public IEntityState WithParts(IEnumerable<Part> parts) =>
            new EntityState(parts, Position, TickEnergy, CurrentInstructionIndex);
        public IEntityState AddTickEnergy(float delta) =>
            new EntityState(Parts, Position, TickEnergy + delta, CurrentInstructionIndex);
        public IEntityState SubtractTickEnergy(float delta) => AddTickEnergy(-delta);
        public IEntityState ResetTickEnergy() => new EntityState(Parts, Position, 0, CurrentInstructionIndex);
        public IEntityState WithCurrentInstructionIndex(int index) =>
            new EntityState(Parts, Position, TickEnergy, index);
    }
}