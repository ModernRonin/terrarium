using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class MouseDelta
    {
        MouseState mLast = Mouse.GetState();
        public Vector2 PointerDelta { get; private set; }
        public float WheelDelta { get; private set; }
        public bool IsLeftDown { get; private set; }
        public bool IsRightDown { get; private set; }
        public bool IsMiddleDown { get; private set; }
        public void Update()
        {
            var current = Mouse.GetState();

            var dx = current.X - mLast.X;
            var dy = current.Y - mLast.Y;
            PointerDelta = new Vector2(dx, dy);
            WheelDelta = current.ScrollWheelValue - mLast.ScrollWheelValue;
            IsLeftDown = current.LeftButton == ButtonState.Pressed;
            IsRightDown = current.RightButton == ButtonState.Pressed;
            IsMiddleDown = current.MiddleButton == ButtonState.Pressed;

            mLast = current;
        }
    }
}