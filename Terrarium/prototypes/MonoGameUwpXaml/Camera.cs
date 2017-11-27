using Microsoft.Xna.Framework;

namespace MonoGameUwpXaml
{
    public class Camera
    {
        Vector2 mPosition;
        float mRotation;
        float mZoom = 1f;
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        Vector2 ViewportCenter => new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
        public Matrix TranslationMatrix => Matrix.CreateTranslation(-(int) mPosition.X, -(int) mPosition.Y, 0) *
                                           Matrix.CreateRotationZ(mRotation) *
                                           Matrix.CreateScale(new Vector3(mZoom, mZoom, 1)) *
                                           Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));

        public void AdjustZoom(float amount)
        {
            mZoom = mZoom + amount;
            if (mZoom < 0.01f) mZoom = 0.001f;
        }

        // Move the camera in an X and Y amount based on the cameraMovement param.
        // if clampToMap is true the camera will try not to pan outside of the
        // bounds of the map.
        public void MoveCamera(Vector2 cameraMovement) => mPosition = mPosition + cameraMovement;
        public void CenterOn(Vector2 position) => mPosition = position;
    }
}