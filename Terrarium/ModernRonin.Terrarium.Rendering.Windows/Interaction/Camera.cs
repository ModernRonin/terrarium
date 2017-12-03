using Microsoft.Xna.Framework;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Rendering.Windows.Utilities;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public class Camera : ICamera
    {
        Vector2 mPosition;
        Vector2 ViewportCenter => new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
        Matrix InverserTransformationMatrix => Matrix.Invert(TransformationMatrix);
        public float Zoom { get; private set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public Matrix TransformationMatrix => Matrix.CreateTranslation(-(int) mPosition.X, -(int) mPosition.Y, 0) *
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
        public void Center() => CenterOn(ViewportCenter);
        public Vector2D ScreenToWorldCoordinates(Vector2 screen) =>
            Vector2.Transform(screen, InverserTransformationMatrix).ToVector2D();
        public Vector2 WorldToScreenCoordinates(Vector2D world) =>
            Vector2.Transform(world.ToVector2(), TransformationMatrix);
    }
}