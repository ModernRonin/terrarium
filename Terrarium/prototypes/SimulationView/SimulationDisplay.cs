using System.Windows;
using System.Windows.Media;
using ModernRonin.Terrarium.Logic;

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
        public SimulationState SimulationState { get; set; } = new SimulationState();
        Size DesiredDisplaySize
        {
            get
            {
                var result = 10 * SimulationState.Size;
                return result.ToSize();
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            Render();
            base.OnRender(drawingContext);
            drawingContext.DrawDrawing(mBackPage);
        }
        void Render()
        {
            using (var renderer = new Renderer(mBackPage, DesiredDisplaySize, SimulationState)) { renderer.Render(); }
        }
    }
}