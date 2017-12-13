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
            float storedEnergy = 0,
            int currentInstructionIndex = 0,
            bool areThrustersOn = false,
            Vector2D thrustDirection = new Vector2D(),
            float lastDistanceMovedSquared = 0)
        {
            Position = position;
            TickEnergy = tickEnergy;
            StoredEnergy = storedEnergy;
            CurrentInstructionIndex = currentInstructionIndex;
            AreThrustersOn = areThrustersOn;
            ThrustDirection = thrustDirection;
            LastDistanceMovedSquared = lastDistanceMovedSquared;
            Parts = parts;
        }
        public Vector2D Position { get; }
        public IEnumerable<Part> Parts { get; }
        public float TickEnergy { get; }
        public float StoredEnergy { get; }
        public int CurrentInstructionIndex { get; }
        public bool AreThrustersOn { get; }
        public Vector2D ThrustDirection { get; }
        public float LastDistanceMovedSquared { get; }
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
        public IEntityState At(Vector2D position) => new EntityState(Parts,
            position,
            TickEnergy,
            StoredEnergy,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState WithParts(IEnumerable<Part> parts) => new EntityState(parts,
            Position,
            TickEnergy,
            StoredEnergy,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState AddTickEnergy(float delta) => new EntityState(Parts,
            Position,
            TickEnergy + delta,
            StoredEnergy,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState SubtractTickEnergy(float delta) => AddTickEnergy(-delta);
        public IEntityState ResetTickEnergy() => new EntityState(Parts,
            Position,
            0,
            StoredEnergy,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState WithCurrentInstructionIndex(int index) => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy,
            index,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState NextInstructionIndex() => WithCurrentInstructionIndex(CurrentInstructionIndex + 1);
        public IEntityState AddStoredEnergy(float delta) => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy + delta,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState SubtractStoredEnergy(float delta) => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy - delta,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState ThrustOn() => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy,
            CurrentInstructionIndex,
            true,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState ThrustOff() => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy,
            CurrentInstructionIndex,
            false,
            ThrustDirection,
            LastDistanceMovedSquared);
        public IEntityState WithThrustDirection(Vector2D direction) => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy,
            CurrentInstructionIndex,
            AreThrustersOn,
            direction,
            LastDistanceMovedSquared);
        public IEntityState WithLastDistanceMovedSquared(float squaredDistance) => new EntityState(Parts,
            Position,
            TickEnergy,
            StoredEnergy,
            CurrentInstructionIndex,
            AreThrustersOn,
            ThrustDirection,
            squaredDistance);
    }
}