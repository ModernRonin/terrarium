using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class CameraController
    {
        const float ZoomSpeed = 0.001f;
        readonly Camera mCamera;
        readonly KeyboardDelta mKeyboard = new KeyboardDelta();
        readonly MouseDelta mMouse = new MouseDelta();
        readonly float mPanSpeed = 1f;
        public CameraController(Camera camera)
        {
            mCamera = camera;
            mCamera.AdjustZoom(5);
        }
        public void Update()
        {
            mMouse.Update();
            if (mMouse.IsLeftDown)
            {
                var movement = -mPanSpeed * mMouse.PointerDelta;
                mCamera.MoveCamera(movement);
            }
            var zoom = ZoomSpeed * mMouse.WheelDelta;
            mCamera.AdjustZoom(zoom);

            mKeyboard.Update();

            if (mKeyboard.WasPressed(Keys.C)) mCamera.CenterOn(new Vector2(50, 50));
        }
    }
}