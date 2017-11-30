namespace ModernRonin.Standard.Tests
{
    public static class AssertionExtensions
    {
        public static Vector2DAssertions Should(this Vector2D self) => new Vector2DAssertions(self);
    }
}