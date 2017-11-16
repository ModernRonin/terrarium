using System.Linq;
using System.Windows;
using ModernRonin.PraeterArtem.Functional;

namespace SimulationView.Model
{
    public class SimulationTicker
    {
        readonly SimulationState mCurrent;
        readonly Vector[] mDirections = {new Vector(1, 1), new Vector(-3, -7)};
        readonly SimulationState mNext = new SimulationState();
        int mDirectionIndex;
        public SimulationTicker(SimulationState current)
        {
            mCurrent = current;
            mNext.Size = mCurrent.Size;
            mDirections.UseIn(v => v.Normalize());
        }
        public SimulationState Tick()
        {
            mNext.Entities = mCurrent.Entities.Select(Move).ToArray();
            return mNext;
        }
        Entity Move(Entity old)
        {
            mDirectionIndex = 0 == mDirectionIndex ? 1 : 0;
            var newPosition = (old.Position + mDirections[mDirectionIndex]).WrapOver(mNext.Size);

            // TODO: make Simulation, Entity etc. all immutable so we can't forget to treat them as such
            return new Entity {Parts = old.Parts, Position = newPosition};
        }
    }
}