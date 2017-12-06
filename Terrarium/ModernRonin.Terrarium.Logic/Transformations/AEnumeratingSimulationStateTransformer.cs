﻿using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.Terrarium.Logic.Transformations {
    public abstract class AEnumeratingSimulationStateTransformer<T> : ASimulationStateTransformer<IEnumerable<T>>
    {
        protected override IEnumerable<T> Transform(IEnumerable<T> old, ISimulationState state)
        {
            return old.Select(e => Transform(e, state));
        }
        protected abstract T Transform(T old, ISimulationState state);
    }
}