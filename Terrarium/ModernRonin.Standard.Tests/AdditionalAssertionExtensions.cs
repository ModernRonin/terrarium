namespace ModernRonin.Standard.Tests
{
    public static class AdditionalAssertionExtensions
    {
        public static Vector2DAssertions Should(this Vector2D self) => new Vector2DAssertions(self);
        public static FloatAssertions Should(this float self) => new FloatAssertions(self);
    }
}