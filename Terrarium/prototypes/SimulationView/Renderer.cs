using System.Windows.Media;
using SimulationView.Model;

namespace SimulationView {
    public class Renderer
    {
        public Simulation Simulation { get; set; } = new Simulation();
        public DrawingGroup Root { get; }= new DrawingGroup();
        public void Render()
        {
            
        }
    }
}