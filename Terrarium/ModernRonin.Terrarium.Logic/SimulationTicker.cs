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
            var entities = mCurrent.Entities.Select(Move).ToArray();
            var energySources = mCurrent.EnergySources.Select(Move).ToArray();
            return mCurrent.WithEntities(entities).WithEnergySources(energySources);
        }
        EntityState Move(EntityState old)
        {
            mDirectionIndex = 0 == mDirectionIndex ? 1 : 0;
            var newPosition = (old.Position + mDirections[mDirectionIndex]).ClampWithin(mCurrent.Size);
            return old.At(newPosition);
        }
        EnergySource Move(EnergySource old)
        {
            var newPosition = (old.Position + old.Speed).ClampWithin(mCurrent.Size);
            return old.At(newPosition);
        }
    }
}