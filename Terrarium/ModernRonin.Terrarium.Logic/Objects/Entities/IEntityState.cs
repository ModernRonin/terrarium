﻿using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public interface IEntityState
    {
        Vector2D Position { get; }
        IEnumerable<Part> Parts { get; }
        string Code { get; }
        Rectangle2D LocalBoundingBox { get; }
        Rectangle2D AbsoluteBoundingBox { get; }
        float TickEnergy { get; }
        float StoredEnergy { get; }
        int CurrentInstructionIndex { get; }
        bool AreThrustersOn { get; }
        Vector2D ThrustDirection { get; }
        float LastDistanceMovedSquared { get; }
        IEntityState At(Vector2D position);
        IEntityState WithParts(IEnumerable<Part> parts);
        IEntityState AddTickEnergy(float delta);
        IEntityState SubtractTickEnergy(float delta);
        IEntityState ResetTickEnergy();
        IEntityState WithCurrentInstructionIndex(int index);
        IEntityState NextInstructionIndex();
        IEntityState AddStoredEnergy(float delta);
        IEntityState SubtractStoredEnergy(float delta);
        IEntityState ThrustOn();
        IEntityState ThrustOff();
        IEntityState WithThrustDirection(Vector2D direction);
        IEntityState WithLastDistanceMovedSquared(float squaredDistance);
    }
}