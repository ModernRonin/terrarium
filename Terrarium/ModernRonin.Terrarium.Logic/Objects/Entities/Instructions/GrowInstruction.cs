namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions
{
    public class GrowInstruction : IInstruction
    {
        public PartKind Kind { get; }
        public int OriginPartIndex { get; }
        public int Direction { get; }
    }
}