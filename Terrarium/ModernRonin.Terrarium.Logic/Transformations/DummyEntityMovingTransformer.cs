using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Transformations
{
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