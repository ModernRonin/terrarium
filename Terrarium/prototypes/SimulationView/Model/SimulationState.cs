﻿using System.Collections.Generic;
using System.Windows;
using ModernRonin.PraeterArtem.Functional;

namespace SimulationView.Model
{
    public class SimulationState
    {
        public Size Size { get; set; }
        public IEnumerable<Entity> Entities { get; set; } = Null.Enumerable<Entity>();
        public static SimulationState Default => new SimulationState
        {
            Size = new Size(100, 100),
            Entities = new List<Entity> {Entity.Cross.At(new Vector(10, 10)), Entity.Snake.At(new Vector(90, 90))}
        };
    }
}