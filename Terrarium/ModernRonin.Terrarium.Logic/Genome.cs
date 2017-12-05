using System.Collections.Generic;

namespace ModernRonin.Terrarium.Logic
{
    public class Genome
    {
        public Parameters Parameters { get; }
        public IList<IInstruction> Instructions { get; }
    }

    public class Parameters
    {
        public float HungryThreshold { get; }
        public float FatThreshold { get;  }
        public float MovingQuicklyThreshold { get;  }
        public float MovingSlowlyThreshold { get; }
        public int ManyPartsThreshold { get; }
        public int FewPartsThreshold { get; }
    }

    public interface IInstruction { }

    public class JumpInstruction : IInstruction
    {
        public int InstructionPointerDelta { get; }
    }

    public class JumpIfInstruction : JumpInstruction { }

    public class GrowInstruction : IInstruction { }

    public class RotateThrusterInstruction : IInstruction { }

    public class TurnThrusterOnOrOffInstruction : IInstruction { }

    public interface ICondition { }

    public class AmHungryCondition : ICondition { }

    public class AmFatCondition : ICondition { }

    public class AmMovingCondition : ICondition { }

    public class AmMovingQuicklyCondition : ICondition { }

    public class AmMovingSlowlyCondition : ICondition { }
    public class HavePartsCondition : ICondition { }
}