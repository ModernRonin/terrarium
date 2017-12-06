using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public class SimulationTicker
    {
        readonly ISimulationState mCurrent;
        readonly Vector2D[] mDirections = {new Vector2D(1, 1).Normalized, new Vector2D(-3, -7).Normalized};
        int mDirectionIndex;
        public SimulationTicker(ISimulationState current) => mCurrent = current;
        public ISimulationState Tick()
        {
            var energySources = mCurrent.EnergySources.Select(Move).ToArray();
            var entities = mCurrent.Entities.Select(Move).ToArray();
            return mCurrent.WithEnergySources(energySources).WithEntities(entities);
        }
        Entity Move(Entity entity)
        {
            var old = entity.State;
            mDirectionIndex = 0 == mDirectionIndex ? 1 : 0;
            var newPosition = (old.Position + mDirections[mDirectionIndex]).ClampWithin(mCurrent.Size);
            return entity.WithState(old.At(newPosition));
        }
        IEnergySource Move(IEnergySource old)
        {
            var newPosition = (old.Position + old.Speed).ClampWithin(mCurrent.Size);
            return old.At(newPosition);
        }
    }

    public interface ISimulationTransformer
    {
        ISimulationState Transform(ISimulationState state);
    }

    public abstract class ASimulationTransformer<T> : ISimulationTransformer
    {
        public ISimulationState Transform(ISimulationState state)
        {
            var old = ExtractStateProperty(state);
            var nu = Transform(old, state);
            return SetStateProperty(state, nu);
        }
        protected abstract T Transform(T old, ISimulationState state);
        protected abstract T ExtractStateProperty(ISimulationState state);
        protected abstract ISimulationState SetStateProperty(ISimulationState state, T property);
    }

    public abstract class AEnumeratingSimulationTransformer<T> : ASimulationTransformer<IEnumerable<T>>
    {
        protected override IEnumerable<T> Transform(IEnumerable<T> old, ISimulationState state)
        {
            return old.Select(e => Transform(e, state));
        }
        protected abstract T Transform(T old, ISimulationState state);
    }

    public abstract class AnEnergySourceTransformer : AEnumeratingSimulationTransformer<IEnergySource>
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

    public abstract class AnEntityTransformer : AEnumeratingSimulationTransformer<Entity>
    {
        protected override IEnumerable<Entity> ExtractStateProperty(ISimulationState state)
        {
            return state.Entities;
        }
        protected override ISimulationState SetStateProperty(ISimulationState state, IEnumerable<Entity> property)
        {
           return  state.WithEntities(property);
        }
    }
    public class DummyEntityMovingTransformer : AnEntityTransformer {
        readonly Vector2D[] mDirections = { new Vector2D(1, 1).Normalized, new Vector2D(-3, -7).Normalized };
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