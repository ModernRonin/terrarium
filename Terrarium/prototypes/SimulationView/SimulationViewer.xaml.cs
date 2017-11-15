using System.Windows.Media;

namespace SimulationView
{
    public partial class SimulationViewer
    {
        
        public SimulationViewer()
        {
            InitializeComponent();
        }
        public Renderer Renderer { get; set; }= new Renderer();
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Renderer.Render();
            drawingContext.DrawDrawing(Renderer.Root);
        }
    }
}