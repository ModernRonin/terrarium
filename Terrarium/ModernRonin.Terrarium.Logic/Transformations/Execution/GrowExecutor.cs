﻿using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;
using MoreLinq;
using static ModernRonin.Terrarium.Logic.Utilities.SimulationGeometry;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class GrowExecutor : AnInstructionExecutor<GrowInstruction>
    {
        readonly IEntityMidwife mMidwife;
        public GrowExecutor(IEntityMidwife midwife) => mMidwife = midwife;
        protected override ISimulationState DoExecute(
            GrowInstruction instruction,
            IEntity entity,
            ISimulationState simulationState)
        {
            var entityState = entity.State.NextInstructionIndex();
            var frozenParts = entityState.Parts.ToList();
            var direction = VectorFor(instruction.Direction);
            var origin = frozenParts.ElementAt(instruction.OriginPartIndex % frozenParts.Count).RelativePosition;
            var occupiedBySelf = frozenParts.Select(p => new Rectangle2D(p.RelativePosition, new Vector2D(1, 1)))
                                            .ToArray();
            var targetPosition = FindNextUnoccupiedPoint(origin, direction, occupiedBySelf);

            var isTargetPositionFree = simulationState
                .CollisionDetection.Excepting(occupiedBySelf).IsFreeAt(entityState.Position + targetPosition);
            if (isTargetPositionFree)
                if (instruction.Kind == PartKind.Core)
                    simulationState = Reproduce(entity, targetPosition, simulationState);
                else
                    entityState =
                        entityState.WithParts(frozenParts.InsertPartPushing(new Part(instruction.Kind, origin),
                            direction,
                            targetPosition));
            return simulationState.ReplaceEntity(entity, entity.WithState(entityState));
        }
        ISimulationState Reproduce(IEntity parent, Vector2D targetPosition, ISimulationState simulationState) =>
            simulationState.WithEntities(mMidwife.GiveBirth(parent, targetPosition).Concat(simulationState.Entities));
    }
}