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
        readonly Pen mPen = new Pen(new SolidColorBrush(Colors.White), 1);
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
            context.DrawRectangle(Brushes.Black, new Pen(), new Rect(RenderSize));
            Simulation.Entities.UseIn(e => Draw(context, e));
        }
        void Draw(DrawingContext context, Entity entity)
        {
            void drawRectangle(ColoredRectangle rectangle)
            {
                context.DrawRectangle(new SolidColorBrush(rectangle.Color), null, rectangle.Rectangle);
            }

            entity.Parts.Select(p => ToRectangle(p, entity.Position)).UseIn(drawRectangle);
        }
        ColoredRectangle ToRectangle(Part part, Vector origin)
        {
            var factor = new Vector(RenderSize.Width / Simulation.Size.Width,
                RenderSize.Height / Simulation.Size.Height);
            var position = (origin + part.RelativePosition).ScaleBy(factor);
            return new ColoredRectangle
            {
                Color = ToColor(part.Kind),
                Rectangle = new Rect((Point) position, (Size) factor)
            };
        }
        static Color ToColor(PartKind kind)
        {
            switch (kind)
            {
                case PartKind.Core:
                    return Colors.Aquamarine;

                case PartKind.Absorber:
                    return Colors.LightGreen;
                case PartKind.Store:
                    return Colors.BurlyWood;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        class ColoredRectangle
        {
            public Rect Rectangle { get; set; }
            public Color Color { get; set; }
        }
    }

    public static class VectorExtensions
    {
        public static Vector ScaleBy(this Vector self, Vector scale) => new Vector(self.X * scale.X, self.Y * scale.Y);
    }
}