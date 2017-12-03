﻿using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulationState
    {
        Vector2D Size { get; }
        IEnumerable<Entity> Entities { get; }
        float[,] EnergyDensity { get; }
        IEnumerable<EnergySource> EnergySources { get; }
        IEnumerable<Entity> GetEntityAt(Vector2D position);
    }
}