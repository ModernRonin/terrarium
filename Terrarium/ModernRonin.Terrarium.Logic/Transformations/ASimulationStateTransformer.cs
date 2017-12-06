using System;
using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public abstract class ASimulationStateTransformer<T> : ISimulationStateTransformerWithDependencies
    {
        protected static IEnumerable<Type> NoTypes => Null.Enumerable<Type>();
        public ISimulationState Transform(ISimulationState state)
        {
            var old = Get(state);
            var nu = Transform(old, state);
            return Set(state, nu);
        }
        public abstract IEnumerable<Type> Dependencies { get; }
        protected abstract T Transform(T old, ISimulationState state);
        protected abstract T Get(ISimulationState state);
        protected abstract ISimulationState Set(ISimulationState state, T property);
    }
}