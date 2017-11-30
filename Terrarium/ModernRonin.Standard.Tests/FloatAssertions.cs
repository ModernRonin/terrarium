using FluentAssertions;
using FluentAssertions.Numeric;

namespace ModernRonin.Standard.Tests
{
    public class FloatAssertions : NumericAssertions<float>
    {
        public FloatAssertions(object value) : base(value) { }
        public void Approximate(float expected)
        {
            ((float) Subject).Should().BeApproximately(expected, 0.001f);
        }
    }
}