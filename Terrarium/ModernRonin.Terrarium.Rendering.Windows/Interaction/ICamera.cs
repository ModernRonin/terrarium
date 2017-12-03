using Microsoft.Xna.Framework;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public interface ICamera
    {
        float Zoom { get; }
        int ViewportWidth { get; set; }
        int ViewportHeight { get; set; }
        Matrix TransformationMatrix { get; }
        void AdjustZoom(float amount);
        void MoveCamera(Vector2 cameraMovement);
        void CenterOn(Vector2 position);
        void Center();
        Vector2D ScreenToWorldCoordinates(Vector2 screen);
        Vector2 WorldToScreenCoordinates(Vector2D world);
    }
}