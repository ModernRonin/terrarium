﻿using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulationState
    {
        Vector2D Size { get; }
        IEnumerable<Entity> Entities { get; }
        float[,] EnergyDensity { get; }
        IEnumerable<IEnergySource> EnergySources { get; }
        IEnumerable<Entity> GetEntitiesAt(Vector2D position);
        ISimulationState WithEnergySources(IEnumerable<IEnergySource> energySources);
        ISimulationState WithEntities(IEnumerable<Entity> entities);
    }
}