using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class GrowExecutor : AnInstructionExecutor<GrowInstruction>
    {
        static readonly Vector2D[] sDirectionVectors =
        {
            new Vector2D(0, -1), new Vector2D(1, -1), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
            new Vector2D(-1, 0), new Vector2D(-1, -1), new Vector2D(-1, 1)
        };
        protected override ISimulationState DoExecute(
            GrowInstruction instruction,
            IEntity entity,
            ISimulationState simulationState)
        {
            var entityState = entity.State.NextInstructionIndex();
            var frozenParts = entityState.Parts.ToList();
            var direction = sDirectionVectors[instruction.Direction % sDirectionVectors.Length];
            var origin = frozenParts.ElementAt(instruction.OriginPartIndex % frozenParts.Count).RelativePosition;
            var occupiedBySelf = frozenParts.Select(p => new Rectangle2D(p.RelativePosition, new Vector2D(1, 1)))
                                            .ToArray();
            var targetPosition = FindNextSpaceNotOccupiedBySelf(origin, direction, occupiedBySelf);

            var isTargetPositionFree = simulationState
                .CollisionDetection.Excepting(occupiedBySelf).IsFreeAt(entityState.Position + targetPosition);
            if (isTargetPositionFree)
            {
                if (instruction.Kind != PartKind.Core)
                {
                    var points = PointsFromTo(origin, targetPosition, direction);
                    var partsToMove = FilterPartsAtPoints(frozenParts, points);

                    Part shift(Part part) => new Part(part.Kind, part.RelativePosition + direction);

                    frozenParts = frozenParts.Replace(partsToMove, shift).ToList();
                }
                targetPosition = instruction.Kind == PartKind.Core ? targetPosition : origin;
                frozenParts.Add(new Part(instruction.Kind, targetPosition));
                entityState = entityState.WithParts(frozenParts);
            }
            return simulationState.ReplaceEntity(entity, entity.WithState(entityState));
        }
        Vector2D FindNextSpaceNotOccupiedBySelf(Vector2D start, Vector2D direction, Rectangle2D[] occupied)
        {
            if (!occupied.Any(o => o.Contains(start))) return start;
            return FindNextSpaceNotOccupiedBySelf(start + direction, direction, occupied);
        }
        IEnumerable<Vector2D> PointsFromTo(Vector2D start, Vector2D end, Vector2D increment)
        {
            while (start != end)
            {
                yield return start;
                start += increment;
            }
        }
        IEnumerable<Part> FilterPartsAtPoints(IEnumerable<Part> parts, IEnumerable<Vector2D> points) => parts.Where(p =>
        {
            var boundingBox = new Rectangle2D(p.RelativePosition, new Vector2D(1, 1));
            return points.Any(boundingBox.Contains);
        });
    }
}