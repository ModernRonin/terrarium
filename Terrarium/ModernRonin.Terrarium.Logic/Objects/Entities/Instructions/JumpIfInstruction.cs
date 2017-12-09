using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;

namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions
{
    public class JumpIfInstruction : JumpInstruction
    {
        public JumpIfInstruction(int instructionPointerDelta, ICondition condition) : base(instructionPointerDelta) =>
            Condition = condition;
        public ICondition Condition { get; }
    }
}