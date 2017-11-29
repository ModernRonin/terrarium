using Microsoft.Xna.Framework;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class Camera
    {
        Vector2 mPosition;
        public float Zoom { get; private set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        Vector2 ViewportCenter => new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
        public Matrix TranslationMatrix => Matrix.CreateTranslation(-(int) mPosition.X, -(int) mPosition.Y, 0) *
                                           Matrix.CreateRotationZ(0) *
                                           Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                           Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
        public void AdjustZoom(float amount)
        {
            Zoom = Zoom + amount;
            if (Zoom < 0.001f) Zoom = 0.001f;
        }
        public void MoveCamera(Vector2 cameraMovement) => mPosition = mPosition + cameraMovement;
        public void CenterOn(Vector2 position) => mPosition = position;
    }
}