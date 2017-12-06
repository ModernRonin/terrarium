﻿using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface IEnergyCostConfiguration
    {
        float GetEnergyCostForPartKind(PartKind kind);
        float GetEnergyCostForInstruction(IInstruction instruction);
    }
}