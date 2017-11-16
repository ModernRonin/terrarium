using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ModernRonin.PraeterArtem.Functional;
using SimulationView.Model;

namespace SimulationView
{
    public class Renderer: IDisposable
    {
        readonly DrawingContext mContext;
        readonly Size mRenderSize;
        readonly Vector mScalingFactor;
        readonly Simulation mSimulation;
        public Renderer(DrawingGroup drawingGroup, Size renderSize, Simulation simulation)
        {
            mContext = drawingGroup.Open();
            mRenderSize = renderSize;
            mSimulation = simulation;
            mScalingFactor = mSimulation.Size.ScaleTo(mRenderSize);
        }
        public void Dispose()
        {
            mContext.Close();
        }
        public void Render()
        {
            mContext.DrawRectangle(Brushes.Black, null, new Rect(mRenderSize));
            mSimulation.Entities.UseIn(Draw);
        }
        void Draw(Entity entity)
        {
            void drawRectangle(FilledRectangle rectangle)
            {
                mContext.DrawRectangle(rectangle.Brush, null, rectangle.Rectangle);
            }

            entity.Parts.Select(p => ToRectangle(p, entity.Position)).UseIn(drawRectangle);
        }
        FilledRectangle ToRectangle(Part part, Vector origin)
        {
            var position = (origin + part.RelativePosition).ScaleBy(mScalingFactor);
            return new FilledRectangle
            {
                Brush = ToColor(part.Kind),
                Rectangle = new Rect((Point) position, (Size) mScalingFactor)
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