using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class MouseDelta : IUpdateable
    {
        MouseState mLast = Mouse.GetState();
        DateTime mLastLeftDown = DateTime.MinValue;
        DateTime mLastLeftUp = DateTime.MinValue;
        DateTime mLastMiddleDown = DateTime.MinValue;
        DateTime mLastMiddleUp = DateTime.MinValue;
        DateTime mLastRightDown = DateTime.MinValue;
        DateTime mLastRightUp = DateTime.MinValue;
        public Vector2 PointerDelta { get; private set; }
        public float WheelDelta { get; private set; }
        public bool HasWheelMoved => Math.Abs(WheelDelta) > 0.0001f;
        public bool IsLeftDown { get; private set; }
        public bool IsRightDown { get; private set; }
        public bool IsMiddleDown { get; private set; }
        public bool HasLeftBeenClicked => IsInClickRange(mLastLeftUp.Subtract(mLastLeftDown));
        public bool HasRightBeenClicked => IsInClickRange(mLastRightUp.Subtract(mLastRightDown));
        public bool HasMiddleBeenClicked => IsInClickRange(mLastMiddleUp.Subtract(mLastMiddleDown));
        public Vector2 PointerPosition { get; private set; }
        bool IsInClickRange(TimeSpan delta)
        {
            if (delta.TotalMilliseconds < 0) return false;
            return delta.TotalMilliseconds < 1000;
        }
        public void Update()
        {
            var current = Mouse.GetState();
            PointerPosition = new Vector2(current.X, current.Y);
            var dx = current.X - mLast.X;
            var dy = current.Y - mLast.Y;
            PointerDelta = new Vector2(dx, dy);
            WheelDelta = current.ScrollWheelValue - mLast.ScrollWheelValue;
            var now = DateTime.Now;

            if (current.LeftButton == ButtonState.Released) mLastLeftUp = now;
            else mLastLeftDown = now;
            if (current.RightButton == ButtonState.Released) mLastRightUp = now;
            else mLastRightDown = now;
            if (current.MiddleButton == ButtonState.Released) mLastMiddleUp = now;
            else mLastMiddleDown = now;

            IsLeftDown = current.LeftButton == ButtonState.Pressed;
            IsRightDown = current.RightButton == ButtonState.Pressed;
            IsMiddleDown = current.MiddleButton == ButtonState.Pressed;
            mLast = current;
        }
    }
}