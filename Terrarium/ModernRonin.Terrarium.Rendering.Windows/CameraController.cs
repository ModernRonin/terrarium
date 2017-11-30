﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class CameraController
    {
        const float ZoomSpeed = 0.001f;
        readonly Camera mCamera;
        readonly KeyboardDelta mKeyboard = new KeyboardDelta();
        readonly MouseDelta mMouse = new MouseDelta();
        float mPanSpeed = 1f;
        public CameraController(Camera camera)
        {
            mCamera = camera;
            mCamera.AdjustZoom(5);
            Center();
        }
        public void Update()
        {
            mMouse.Update();
            if (mMouse.IsLeftDown) mCamera.MoveCamera(-mPanSpeed * mMouse.PointerDelta);
            if (mMouse.HasWheelMoved)
            {
                mCamera.AdjustZoom(ZoomSpeed * mMouse.WheelDelta);
                mPanSpeed = 0.7f / mCamera.Zoom;
            }

            mKeyboard.Update();

            if (mKeyboard.WasPressed(Keys.C)) Center();
            if (mKeyboard.WasPressed(Keys.OemPlus)) mPanSpeed += 0.1f;
            if (mKeyboard.WasPressed(Keys.OemMinus)) mPanSpeed -= 0.1f;
        }
        void Center()
        {
            mCamera.CenterOn(new Vector2(50, 50));
        }
    }
}