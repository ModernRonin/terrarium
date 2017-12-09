namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions
{
    public class JumpInstruction : IInstruction
    {
        public JumpInstruction(int instructionPointerDelta) => InstructionPointerDelta = instructionPointerDelta;
        public int InstructionPointerDelta { get; }
    }
}