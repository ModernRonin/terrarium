using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct Part
    {
        public Part(PartKind kind, Vector2D relativePosition)
        {
            RelativePosition = relativePosition;
            Kind = kind;
        }
        public Vector2D RelativePosition { get; }
        public PartKind Kind { get; }
    }
}