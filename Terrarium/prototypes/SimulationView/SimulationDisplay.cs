using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ModernRonin.PraeterArtem.Functional;
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
            var context = mBackPage.Open();
            Render(context);
            context.Close();
        }
        void Render(DrawingContext context)
        {
            context.DrawRectangle(Brushes.Black, null, new Rect(RenderSize));
            Simulation.Entities.UseIn(e => Draw(context, e));
        }
        void Draw(DrawingContext context, Entity entity)
        {
            void drawRectangle(FilledRectangle rectangle)
            {
                context.DrawRectangle(rectangle.Brush, null, rectangle.Rectangle);
            }

            entity.Parts.Select(p => ToRectangle(p, entity.Position)).UseIn(drawRectangle);
        }
        FilledRectangle ToRectangle(Part part, Vector origin)
        {
            var factor = new Vector(RenderSize.Width / Simulation.Size.Width,
                RenderSize.Height / Simulation.Size.Height);
            var position = (origin + part.RelativePosition).ScaleBy(factor);
            return new FilledRectangle
            {
                Brush = ToColor(part.Kind),
                Rectangle = new Rect((Point) position, (Size) factor)
            };
        }
        static Brush ToColor(PartKind kind)
        {
            switch (kind)
            {
                case PartKind.Core:
                    return Brushes.Aquamarine;

                case PartKind.Absorber:
                    return Brushes.LightGreen;
                case PartKind.Store:
                    return Brushes.BurlyWood;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        class FilledRectangle
        {
            public Rect Rectangle { get; set; }
            public Brush Brush { get; set; }
        }
    }
}