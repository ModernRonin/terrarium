using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions
{
    public class RotateThrustersInstruction : IInstruction
    {
        public RotateThrustersInstruction(Vector2D newRotation) => NewRotation = newRotation;
        public Vector2D NewRotation { get; }
    }
}