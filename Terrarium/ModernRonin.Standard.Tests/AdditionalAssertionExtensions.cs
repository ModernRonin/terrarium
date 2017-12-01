using System.Collections.Generic;

namespace ModernRonin.Standard.Tests
{
    public static class AdditionalAssertionExtensions
    {
        public static Vector2DAssertions OughtTo(this Vector2D self) => new Vector2DAssertions(self);
        public static RectangleAssertions OughtTo(this Rectangle2D self) => new RectangleAssertions(self);
        public static FloatAssertions OughtTo(this float self) => new FloatAssertions(self);
        public static void ShouldAllApproximate(this IEnumerable<float> self, float expected)
        {
            foreach (var element in self) element.OughtTo().Approximate(expected);
        }
    }
}