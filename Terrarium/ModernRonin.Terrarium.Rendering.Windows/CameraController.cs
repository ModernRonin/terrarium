using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows {
    public class CameraController
    {
        const float ZoomSpeed = 0.001f;
        const int PanSpeed = 1;
        readonly Camera mCamera;
        KeyboardState mLastKeyboardState= Keyboard.GetState();
        MouseState mLastMouseState = Mouse.GetState();
        public CameraController(Camera camera)
        {
            mCamera = camera;
            mCamera.AdjustZoom(5);
        }
        public void Update()
        {
            var currentMouseState = Mouse.GetState();
            var currentKeyboardState = Keyboard.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                var dx = currentMouseState.X - mLastMouseState.X;
                var dy = currentMouseState.Y - mLastMouseState.Y;
                var movement = new Vector2(-dx * PanSpeed, -dy * PanSpeed);
                mCamera.MoveCamera(movement);
            }
            var zoom = ZoomSpeed * (currentMouseState.ScrollWheelValue - mLastMouseState.ScrollWheelValue);
            mCamera.AdjustZoom(zoom);
            if (mLastKeyboardState.IsKeyDown(Keys.C) && currentKeyboardState.IsKeyUp(Keys.C))
                mCamera.CenterOn(new Vector2(50, 50));

            mLastMouseState = currentMouseState;
            mLastKeyboardState = currentKeyboardState;

        }
    }
}