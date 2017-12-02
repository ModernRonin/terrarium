using Microsoft.Xna.Framework;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public interface ICamera
    {
        float Zoom { get; }
        int ViewportWidth { get; set; }
        int ViewportHeight { get; set; }
        Matrix TranslationMatrix { get; }
        void AdjustZoom(float amount);
        void MoveCamera(Vector2 cameraMovement);
        void CenterOn(Vector2 position);
        void Center();
    }
}