using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class MouseDelta
    {
        MouseState mLast = Mouse.GetState();
        public Vector2 PointerDelta { get; private set; }
        public float WheelDelta { get; private set; }
        public bool HasWheelMoved => Math.Abs(WheelDelta) > 0.0001f;
        public bool IsLeftDown { get; private set; }
        public bool IsRightDown { get; private set; }
        public bool IsMiddleDown { get; private set; }
        public bool HasLeftBeenClicked { get; private set; }
        public bool HasRightBeenClicked { get; private set; }
        public bool HasMiddleBeenClicked { get; private set; }
        public Vector2 PointerPosition { get; private set; }
        public void Update()
        {
            var current = Mouse.GetState();
            PointerPosition= new Vector2(current.X, current.Y);
            var dx = current.X - mLast.X;
            var dy = current.Y - mLast.Y;
            PointerDelta = new Vector2(dx, dy);
            WheelDelta = current.ScrollWheelValue - mLast.ScrollWheelValue;
            HasLeftBeenClicked = IsLeftDown && current.LeftButton == ButtonState.Released;
            HasRightBeenClicked = IsRightDown && current.RightButton == ButtonState.Released;
            HasMiddleBeenClicked = IsMiddleDown && current.MiddleButton == ButtonState.Released;

            IsLeftDown = current.LeftButton == ButtonState.Pressed;
            IsRightDown = current.RightButton == ButtonState.Pressed;
            IsMiddleDown = current.MiddleButton == ButtonState.Pressed;
            mLast = current;
        }
    }
}