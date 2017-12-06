using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Part
    {
        public Part(PartKind kind, Vector2D relativePosition)
        {
            RelativePosition = relativePosition;
            Kind = kind;
        }
        public Vector2D RelativePosition { get; }
        public PartKind Kind { get; }
        public string Code => $"{Kind}!{RelativePosition.X}!{RelativePosition.Y}";
    }
}