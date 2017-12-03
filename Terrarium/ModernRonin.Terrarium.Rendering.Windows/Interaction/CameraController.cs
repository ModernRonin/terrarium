using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class CameraController : ICameraController
    {
        const float ZoomSpeed = 0.001f;
        readonly ICamera mCamera;
        readonly KeyboardDelta mKeyboard;
        readonly MouseDelta mMouse;
        float mPanSpeed;
        public CameraController(ICamera camera, KeyboardDelta keyboard, MouseDelta mouse)
        {
            mCamera = camera;
            mKeyboard = keyboard;
            mMouse = mouse;
            mCamera.AdjustZoom(5);
            AdjustPanSpeed();
            Center();
        }
        public void Update()
        {
            mMouse.Update();
            if (mMouse.IsRightDown) mCamera.MoveCamera(-mPanSpeed * mMouse.PointerDelta);
            if (mMouse.HasWheelMoved)
            {
                mCamera.AdjustZoom(ZoomSpeed * mMouse.WheelDelta);
                AdjustPanSpeed();
            }

            mKeyboard.Update();

            if (mKeyboard.WasPressed(Keys.C)) Center();
            if (mKeyboard.WasPressed(Keys.OemPlus)) mPanSpeed += 0.1f;
            if (mKeyboard.WasPressed(Keys.OemMinus)) mPanSpeed -= 0.1f;
        }
        void AdjustPanSpeed()
        {
            mPanSpeed = 0.7f / mCamera.Zoom;
        }
        void Center()
        {
            mCamera.CenterOn(new Vector2(50, 50));
        }
    }
}