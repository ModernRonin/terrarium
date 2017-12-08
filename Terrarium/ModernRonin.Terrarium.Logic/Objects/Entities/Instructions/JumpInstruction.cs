namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions
{
    public class JumpInstruction : IEntityChangingInstruction
    {
        public int InstructionPointerDelta { get; }
    }
}