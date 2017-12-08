using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.Terrarium.Logic.Transformations.Framework
{
    public abstract class AnEnumeratingSimulationStateTransformer<T> : ASimulationStateTransformer<IEnumerable<T>>
    {
        protected sealed override IEnumerable<T> Transform(IEnumerable<T> old, ISimulationState state)
        {
            return old.Select(e => Transform(e, state));
        }
        protected abstract T Transform(T old, ISimulationState state);
    }
}