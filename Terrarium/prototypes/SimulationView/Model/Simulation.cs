using System.Collections.Generic;
using System.Windows;

namespace SimulationView.Model
{
    public class Simulation
    {
        public int Width { get; } = 100;
        public int Height { get; } = 100;
        public IList<Entity> Entities { get; } =
            new List<Entity> {Entity.Cross.At(new Vector(10, 10)), Entity.Snake.At(new Vector(90, 90))};
    }
}