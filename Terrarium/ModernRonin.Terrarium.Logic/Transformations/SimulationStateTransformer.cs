using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class SimulationStateTransformer : ISimulationStateTransformer
    {
        readonly IEnumerable<Func<ISimulationStateTransformerWithDependencies>> mTransformerFactories;
        public SimulationStateTransformer(
            IEnumerable<Func<ISimulationStateTransformerWithDependencies>> transformerFactories) =>
            mTransformerFactories = transformerFactories;
        public ISimulationState Transform(ISimulationState state)
        {
            var transformers = mTransformerFactories.Select(t => t());
            // TODO: if profiling shows it's worth it, we could cache the type sorting
            var sortedByDependencies = transformers.SortByDependencies();
            return sortedByDependencies.Aggregate(state, (s, t) => t.Transform(s));
        }
    }
}