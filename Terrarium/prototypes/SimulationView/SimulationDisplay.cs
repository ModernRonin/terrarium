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
        public Simulation Simulation { get; set; } = new Simulation();
        protected override void OnRender(DrawingContext drawingContext)
        {
            Render();
            base.OnRender(drawingContext);
            drawingContext.DrawDrawing(mBackPage);
        }
        void Render()
        {
            using (var renderer = new Renderer(mBackPage, RenderSize, Simulation)) { renderer.Render(); }
        }
    }
}