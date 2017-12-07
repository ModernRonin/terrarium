using System.Collections.Generic;
using FluentAssertions;
using ModernRonin.Terrarium.Logic.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class WrapAroundIndexableImmutableArrayTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(5, 2)]
        [TestCase(6, 0)]
        [TestCase(7, 1)]
        [TestCase(8, 2)]
        [TestCase(9, 0)]
        public void Indexer_Wraps_Index_And_Delegates_To_Wrappee(int inputIndex, int wrappeeIndex)
        {
            var wrappee = Substitute.For<IReadOnlyList<int>>();
            wrappee.Count.Returns(3);
            wrappee[wrappeeIndex].Returns(19);
            var underTest = new WrapAroundIndexableImmutableArray<int>(wrappee);

            underTest[inputIndex].Should().Be(19);
        }
        [Test]
        public void Count_Delegates_To_Wrappee()
        {
            var wrappee = Substitute.For<IReadOnlyList<int>>();
            wrappee.Count.Returns(17);
            var underTest = new WrapAroundIndexableImmutableArray<int>(wrappee);

            underTest.Count.Should().Be(17);
        }
        [Test]
        public void Enumeration_Delegates_To_Wrappee()
        {
            var wrappee = Substitute.For<IReadOnlyList<int>>();
            var enumerator = Substitute.For<IEnumerator<int>>();
            wrappee.GetEnumerator().Returns(enumerator);
            var underTest = new WrapAroundIndexableImmutableArray<int>(wrappee);

            underTest.GetEnumerator().Should().BeSameAs(enumerator);
        }
    }
}