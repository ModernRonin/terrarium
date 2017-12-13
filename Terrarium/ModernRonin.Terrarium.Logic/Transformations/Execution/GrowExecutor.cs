using System;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public class GrowExecutor : AnInstructionExecutor<GrowInstruction>
    {
        protected override ISimulationState DoExecute(
            GrowInstruction instruction,
            IEntity entity,
            ISimulationState simulationState) => throw new NotImplementedException();
    }
}