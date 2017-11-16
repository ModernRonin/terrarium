using System.Collections.Generic;
using System.Windows;

namespace SimulationView.Model
{
    public class SimulationState
    {
        public Size Size { get; set; } = new Size(100, 100);
        public IEnumerable<Entity> Entities { get; set; } =
            new List<Entity> {Entity.Cross.At(new Vector(10, 10)), Entity.Snake.At(new Vector(90, 90))};
    }
}