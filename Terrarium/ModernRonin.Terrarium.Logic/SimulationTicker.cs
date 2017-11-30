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
            return mCurrent.WithEntities(entities);
        }
        Entity Move(Entity old)
        {
            mDirectionIndex = 0 == mDirectionIndex ? 1 : 0;
            var newPosition = (old.Position + mDirections[mDirectionIndex]).ClampWithin(mCurrent.Size);
            return newPosition.At();
        }
    }
}