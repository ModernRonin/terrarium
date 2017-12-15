using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using MoreLinq;

namespace ModernRonin.Terrarium.Logic.Utilities
{
    public static class ModelExtensions
    {
        public static float[,] ToEnergyDensity(this Vector2D self) => new float[(int) self.X, (int) self.Y];
        public static IEnumerable<Part> OfKind(this IEnumerable<Part> self, PartKind kind) =>
            self.Where(p => p.Kind == kind);
        public static ISimulationState ReplaceEntity(this ISimulationState self, IEntity original, IEntity replacement)
        {
            var entities = replacement.Concat(self.Entities.Except(original.AsEnumerable()));
            return self.WithEntities(entities);
        }
        public static IEntityState WithJump(this IEntityState self, JumpInstruction jump) =>
            self.WithCurrentInstructionIndex(self.CurrentInstructionIndex + jump.InstructionPointerDelta);
        public static IEnumerable<Part> InsertPartPushing(
            this IEnumerable<Part> self,
            Part part,
            Vector2D direction,
            Vector2D targetPosition)
        {
            var frozen = self as Part[] ?? self.ToArray();
            var points = SimulationGeometry.PointsFromTo(part.RelativePosition, direction, targetPosition);
            var partsToMove = frozen.Where(p => points.Any(p.BoundingBox.Contains));

            Part shift(Part p) => new Part(p.Kind, p.RelativePosition + direction);

            return frozen.Replace(partsToMove, shift).Append(part);
        }
    }
}