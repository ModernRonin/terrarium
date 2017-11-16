using System.Windows;
using System.Windows.Media;
using SimulationView.Model;

namespace SimulationView
{
    public class SimulationDisplay : UIElement
    {
        readonly DrawingGroup mBackPage = new DrawingGroup();
        public SimulationDisplay()
        {
            Render();
            CompositionTarget.Rendering += (_, __) => Render();
        }
        public bool DoAutoScale { get; set; }
        
        public Simulation Simulation { get; set; } = new Simulation();
        protected override void OnRender(DrawingContext drawingContext)
        {
            Render();
            base.OnRender(drawingContext);
            drawingContext.DrawDrawing(mBackPage);
        }
        Size DesiredDisplaySize
        {
            get
            {
                if (DoAutoScale) return RenderSize;
                var result = (Vector) Simulation.Size;
                result *= 10;
                return (Size) result;
            }
        }
        void Render()
        {
            using (var renderer = new Renderer(mBackPage, DesiredDisplaySize, Simulation)) { renderer.Render(); }
        }
    }
}