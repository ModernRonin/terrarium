using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using MoreLinq;

namespace ModernRonin.Terrarium.Logic.Utilities
{
    public static class ModelExtensions
    {
        public static float[,] ToEnergyDensity(this Vector2D self) => new float[(int) self.X, (int) self.Y];
        public static IEnumerable<Part> OfKind(this IEnumerable<Part> self, PartKind kind) =>
            self.Where(p => p.Kind == kind);
        public static ISimulationState ReplaceEntity(this ISimulationState self, IEntity original, IEntity replacement)
        {
            var entities = replacement.Concat(self.Entities.Except(original.AsEnumerable()));
            return self.WithEntities(entities);
        }
    }
}