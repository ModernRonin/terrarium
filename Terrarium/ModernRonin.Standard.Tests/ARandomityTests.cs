using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class ARandomityTests
    {
        class Testable : ARandomity
        {
            readonly Queue<double> mDoubles = new Queue<double>();
            public override double Double() => mDoubles.Dequeue();
            public void PushDouble(double rhs)
            {
                mDoubles.Enqueue(rhs);
            }
        }

        public enum SomeEnumeration
        {
            Alpha,
            Bravo,
            Charlie
        }

        [Test]
        [TestCase(0.0d, false)]
        [TestCase(0.1d, false)]
        [TestCase(0.2d, false)]
        [TestCase(0.3d, false)]
        [TestCase(0.4d, false)]
        [TestCase(0.45d, false)]
        [TestCase(0.49d, false)]
        [TestCase(0.5d, true)]
        [TestCase(0.6d, true)]
        [TestCase(0.7d, true)]
        [TestCase(0.8d, true)]
        [TestCase(0.9d, true)]
        [TestCase(0.95d, true)]
        [TestCase(0.99d, true)]
        public void Boolean(double doub, bool expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.Boolean().Should().Be(expected);
        }
        [Test]
        [TestCase(new[] {"a", "b", "c"}, 0.0d, "a")]
        [TestCase(new[] {"a", "b", "c"}, 0.3d, "a")]
        [TestCase(new[] {"a", "b", "c"}, 0.5d, "b")]
        [TestCase(new[] {"a", "b", "c"}, 0.7d, "c")]
        public void ElementOfWithEnumerable(IEnumerable<string> enumerable, double doub, string expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.ElementOf(enumerable).Should().Be(expected);
        }
        [Test]
        [TestCase(new[] {"a", "b", "c"}, 0.0d, "a")]
        [TestCase(new[] {"a", "b", "c"}, 0.3d, "a")]
        [TestCase(new[] {"a", "b", "c"}, 0.5d, "b")]
        [TestCase(new[] {"a", "b", "c"}, 0.7d, "c")]
        public void ElementOfWithList(string[] list, double doub, string expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.ElementOf(list).Should().Be(expected);
        }
        [Test]
        [TestCase(0.0d, SomeEnumeration.Alpha)]
        [TestCase(0.3d, SomeEnumeration.Alpha)]
        [TestCase(0.5d, SomeEnumeration.Bravo)]
        [TestCase(0.8d, SomeEnumeration.Charlie)]
        public void EnumValue(double doub, SomeEnumeration expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.EnumValue<SomeEnumeration>().Should().Be(expected);
        }
        [Test]
        public void EnumValueBadType()
        {
            var underTest = new Testable();
            Action act = () => underTest.EnumValue<string>();
            act.ShouldThrow<ArgumentException>();
        }
        [Test]
        public void Float()
        {
            var underTest = new Testable();
            underTest.PushDouble(0.141d);
            underTest.Float().Should().BeApproximately(0.141f, 0.001f);
        }
        [Test]
        [TestCase(3, 0.3d, 0)]
        [TestCase(3, 0.7d, 2)]
        [TestCase(3, 0.9999d, 2)]
        [TestCase(2, 0.9999d, 1)]
        [TestCase(2, 0.5d, 1)]
        [TestCase(2, 0.3d, 0)]
        [TestCase(2, 0.1d, 0)]
        [TestCase(2, 0.0d, 0)]
        public void IntegerWithMaximum(int exclusiveMaximum, double doub, int expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.Integer(exclusiveMaximum).Should().Be(expected);
        }
        [Test]
        [TestCase(0, 3, 0.3d, 0)]
        [TestCase(0, 3, 0.5d, 1)]
        [TestCase(0, 3, 0.9d, 2)]
        [TestCase(-2, 3, 0.3d, -2)]
        [TestCase(-2, 3, 0.5d, -1)]
        [TestCase(-2, 3, 0.9d, 0)]
        [TestCase(5, 3, 0.3d, 5)]
        [TestCase(5, 3, 0.5d, 6)]
        [TestCase(5, 3, 0.9d, 7)]
        public void IntegerWithMinimumAndMaximum(int inclusiveMinimum, int exclusiveMaximum, double doub, int expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.Integer(inclusiveMinimum, exclusiveMaximum).Should().Be(expected);
        }
        [Test]
        [TestCase(0.3f, 0.5d, false)]
        [TestCase(0.3f, 0.3d, false)]
        [TestCase(0.3f, 0.1d, true)]
        public void IsSmallerThan(float limit, double doub, bool expected)
        {
            var underTest = new Testable();
            underTest.PushDouble(doub);
            underTest.IsSmallerThan(limit).Should().Be(expected);
        }
    }
}