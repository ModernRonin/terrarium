using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ModernRonin.PraeterArtem.Functional;
using SimulationView.Model;

namespace SimulationView
{
    public class Renderer
    {
        readonly Pen mPen = new Pen(new SolidColorBrush(Colors.White), 2);
        public Simulation Simulation { get; set; } = new Simulation();
        public DrawingGroup Root { get; } = new DrawingGroup();
        public void Render()
        {
            var context = Root.Open();
            RenderTo(context);
            context.Close();
        }
        void RenderTo(DrawingContext context)
        {
            Simulation.Entities.UseIn(e => Draw(context, e));
        }
        void Draw(DrawingContext context, Entity entity)
        {
            void drawRectangle(ColoredRectangle rectangle)
            {
                context.DrawRectangle(new SolidColorBrush(rectangle.Color), mPen, rectangle.Rectangle);
            }

            entity.Parts.Select(p => ToRectangle(p, entity.Position)).UseIn(drawRectangle);
        }
        static ColoredRectangle ToRectangle(Part part, Vector2D origin)
        {
            const int factor = 100;
            var o = origin * factor;
            
            return new ColoredRectangle
            {
                Color = ToColor(part.Kind),
                Rectangle = new Rvectorect()
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
}