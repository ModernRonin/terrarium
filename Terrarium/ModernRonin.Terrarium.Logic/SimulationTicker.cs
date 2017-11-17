using System.Linq;

namespace ModernRonin.Terrarium.Logic
{
    public class SimulationTicker
    {
        readonly SimulationState mCurrent;
        readonly Vector2D[] mDirections = {new Vector2D(1, 1).Normalized, new Vector2D(-3, -7).Normalized};
        readonly SimulationState mNext = new SimulationState();
        int mDirectionIndex;
        public SimulationTicker(SimulationState current)
        {
            mCurrent = current;
            mNext.Size = mCurrent.Size;
        }
        public SimulationState Tick()
        {
            mNext.Entities = mCurrent.Entities.Select(Move).ToArray();
            return mNext;
        }
        Entity Move(Entity old)
        {
            mDirectionIndex = 0 == mDirectionIndex ? 1 : 0;
            var newPosition = (old.Position + mDirections[mDirectionIndex]).ClampWithin(mNext.Size);

            // TODO: make Simulation, Entity etc. all immutable so we can't forget to treat them as such
            return new Entity {Parts = old.Parts, Position = newPosition};
        }
    }
}