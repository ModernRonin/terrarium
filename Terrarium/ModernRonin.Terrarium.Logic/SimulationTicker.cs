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
}