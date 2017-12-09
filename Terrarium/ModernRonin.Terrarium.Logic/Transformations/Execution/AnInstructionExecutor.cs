﻿using System;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Transformations.Execution
{
    public abstract class AnInstructionExecutor<T> : IInstructionExecutor where T : IInstruction
    {
        public ISimulationState Execute(IInstruction instruction, IEntity entity, ISimulationState simulationState)
        {
            return DoExecute((T) instruction, entity, simulationState);
            
        }
        public Type HandledInstructionType => typeof(T);
        protected abstract ISimulationState DoExecute(T instruction, IEntity entity, ISimulationState simulationState);
    }
}