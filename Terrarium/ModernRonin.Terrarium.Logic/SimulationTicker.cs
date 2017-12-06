using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulationStateTransformer
    {
        int Priority { get; }
        ISimulationState Transform(ISimulationState state);
    }

    public abstract class ASimulationStateTransformer<T> : ISimulationStateTransformer
    {
        public ISimulationState Transform(ISimulationState state)
        {
            var old = Get(state);
            var nu = Transform(old, state);
            return Set(state, nu);
        }
        public virtual int Priority => 0;
        protected abstract T Transform(T old, ISimulationState state);
        protected abstract T Get(ISimulationState state);
        protected abstract ISimulationState Set(ISimulationState state, T property);
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
        protected override IEnumerable<IEnergySource> Get(ISimulationState state) =>
            state.EnergySources;
        protected override ISimulationState Set(
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
        protected override IEnumerable<Entity> Get(ISimulationState state) => state.Entities;
        protected override ISimulationState Set(ISimulationState state, IEnumerable<Entity> property) =>
            state.WithEntities(property);
    }

    public class DummyEntityMovingTransformer : AnEntityTransformer
    {
        readonly Dictionary<string, Vector2D> mDirectionsForCodes = new Dictionary<string, Vector2D>()
        {
            {Defaults.Cross.Code, new Vector2D(1, 1).Normalized},
            {Defaults.Snake.Code, new Vector2D(-3, -7).Normalized}
        };
        protected override Entity Transform(Entity entity, ISimulationState state)
        {
            var old = entity.State;
            var delta = mDirectionsForCodes[old.Code];
            var newPosition = (old.Position + delta).ClampWithin(state.Size);
            return entity.WithState(old.At(newPosition));
        }
    }
}