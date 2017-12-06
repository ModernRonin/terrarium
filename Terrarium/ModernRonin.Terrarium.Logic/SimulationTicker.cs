using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulationTicker
    {
        ISimulationState Tick(ISimulationState state);
    }

    public class SimulationTicker : ISimulationTicker
    {
        readonly IEnumerable<ISimulationStateTransformer> mTransformers;
        public SimulationTicker(IEnumerable<ISimulationStateTransformer> transformers) => mTransformers = transformers;
        public ISimulationState Tick(ISimulationState state)
        {
            return mTransformers.OrderBy(t => t.Priority).Aggregate(state, (s, t) => t.Transform(s));
        }
    }

    public interface ISimulationStateTransformer
    {
        int Priority { get; }
        ISimulationState Transform(ISimulationState state);
    }

    public abstract class ASimulationStateTransformer<T> : ISimulationStateTransformer
    {
        public ISimulationState Transform(ISimulationState state)
        {
            var old = ExtractStateProperty(state);
            var nu = Transform(old, state);
            return SetStateProperty(state, nu);
        }
        public virtual int Priority => 0;
        protected abstract T Transform(T old, ISimulationState state);
        protected abstract T ExtractStateProperty(ISimulationState state);
        protected abstract ISimulationState SetStateProperty(ISimulationState state, T property);
    }

    public abstract class AEnumeratingSimulationStateTransformer<T> : ASimulationStateTransformer<IEnumerable<T>>
    {
        protected override IEnumerable<T> Transform(IEnumerable<T> old, ISimulationState state)
        {
            return old.Select(e => Transform(e, state));
        }
        protected abstract T Transform(T old, ISimulationState state);
    }

    public abstract class AnEnergySourceTransformer : AEnumeratingSimulationStateTransformer<IEnergySource>
    {
        protected override IEnumerable<IEnergySource> ExtractStateProperty(ISimulationState state) =>
            state.EnergySources;
        protected override ISimulationState SetStateProperty(
            ISimulationState state,
            IEnumerable<IEnergySource> property) => state.WithEnergySources(property);
    }

    public class EnergySourceMovingTransformer : AnEnergySourceTransformer
    {
        protected override IEnergySource Transform(IEnergySource old, ISimulationState state)
        {
            var newPosition = (old.Position + old.Speed).ClampWithin(state.Size);
            return old.At(newPosition);
        }
    }

    public abstract class AnEntityTransformer : AEnumeratingSimulationStateTransformer<Entity>
    {
        protected override IEnumerable<Entity> ExtractStateProperty(ISimulationState state) => state.Entities;
        protected override ISimulationState SetStateProperty(ISimulationState state, IEnumerable<Entity> property) =>
            state.WithEntities(property);
    }

    public class DummyEntityMovingTransformer : AnEntityTransformer
    {
        readonly Vector2D[] mDirections = {new Vector2D(1, 1).Normalized, new Vector2D(-3, -7).Normalized};
        int mDirectionIndex;
        protected override Entity Transform(Entity entity, ISimulationState state)
        {
            var old = entity.State;
            mDirectionIndex = 0 == mDirectionIndex ? 1 : 0;
            var newPosition = (old.Position + mDirections[mDirectionIndex]).ClampWithin(state.Size);
            return entity.WithState(old.At(newPosition));
        }
    }
}