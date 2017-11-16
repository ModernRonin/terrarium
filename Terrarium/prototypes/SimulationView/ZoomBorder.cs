using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimulationView
{
    /// <summary>
    /// taken from https://stackoverflow.com/a/6782715/2105891
    /// </summary>
    public class ZoomBorder : Border
    {
        UIElement child;
        Point origin;
        Point start;
        public override UIElement Child
        {
            get => base.Child;
            set
            {
                if (value != null && value != Child) Initialize(value);
                base.Child = value;
            }
        }
        TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform) ((TransformGroup) element.RenderTransform).Children.First(tr =>
                tr is TranslateTransform);
        }
        ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)
                ((TransformGroup) element.RenderTransform).Children.First(tr => tr is ScaleTransform);
        }
        public void Initialize(UIElement element)
        {
            child = element;
            if (child != null)
            {
                var group = new TransformGroup();
                var st = new ScaleTransform();
                group.Children.Add(st);
                var tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                MouseWheel += child_MouseWheel;
                MouseLeftButtonDown += child_MouseLeftButtonDown;
                MouseLeftButtonUp += child_MouseLeftButtonUp;
                MouseMove += child_MouseMove;
                PreviewMouseRightButtonDown += child_PreviewMouseRightButtonDown;
            }
        }
        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }
        #region Child Events
        void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                var zoom = e.Delta > 0 ? .2 : -.2;
                if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4)) return;

                var relative = e.GetPosition(child);
                double abosuluteX;
                double abosuluteY;

                abosuluteX = relative.X * st.ScaleX + tt.X;
                abosuluteY = relative.Y * st.ScaleY + tt.Y;

                st.ScaleX += zoom;
                st.ScaleY += zoom;

                tt.X = abosuluteX - relative.X * st.ScaleX;
                tt.Y = abosuluteY - relative.Y * st.ScaleY;
            }
        }
        void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }
        void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                Cursor = Cursors.Arrow;
            }
        }
        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Reset();
        }
        void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
                if (child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(child);
                    var v = start - e.GetPosition(this);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }
        }
        #endregion
    }
}