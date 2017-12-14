using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        enum Pet
        {
            Dog,
            Mouse,
            Bird
        }

        [Test]
        public void EnumToDictionary_For_Enum_Values()
        {
            int getPetNameLength(Pet pet) => pet.ToString().Length;
            var result = EnumerableExtensions.EnumToDictionary<Pet, int>(getPetNameLength);
            result.Count.Should().Be(3);
            result[Pet.Dog].Should().Be(3);
            result[Pet.Mouse].Should().Be(5);
            result[Pet.Bird].Should().Be(4);
        }
        [Test]
        public void Replace_Replaces()
        {
            var input = new[] {1, 2, 3, 4, 5};
            var toBeReplaced = new[] {1, 3, 5};
            input.Replace(toBeReplaced, n => n + 10).Should().Equal(11, 2, 13, 4, 15);
        }
    }
}