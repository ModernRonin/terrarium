using System.Windows;

namespace SimulationView.Model
{
    public class Part
    {
        public Vector RelativePosition { get; set; }
        public PartKind Kind { get; set; }
    }
}