using System;
using System.Collections.Generic;
using System.Linq;
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

        class Person : IEquatable<Person>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public static IEqualityComparer<Person> Comparer { get; } = new EqualityComparer();
            public bool Equals(Person other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName);
            }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Person) obj);
            }
            public override int GetHashCode()
            {
                unchecked
                {
                    return (FirstName != null ? FirstName.GetHashCode() : 0) * 397 ^
                           (LastName != null ? LastName.GetHashCode() : 0);
                }
            }
            public static bool operator ==(Person left, Person right) => Equals(left, right);
            public static bool operator !=(Person left, Person right) => !Equals(left, right);

            sealed class EqualityComparer : IEqualityComparer<Person>
            {
                public bool Equals(Person x, Person y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return string.Equals(x.FirstName, y.FirstName) && string.Equals(x.LastName, y.LastName);
                }
                public int GetHashCode(Person obj)
                {
                    unchecked
                    {
                        return (obj.FirstName != null ? obj.FirstName.GetHashCode() : 0) * 397 ^
                               (obj.LastName != null ? obj.LastName.GetHashCode() : 0);
                    }
                }
            }
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
        [Test]
        public void Replace_With_Special_EqualityComparer_Replaces()
        {
            var input = new[]
            {
                new Person {FirstName = "Alpha", LastName = "Bravo"},
                new Person {FirstName = "Charlie", LastName = "Delta"},
                new Person {FirstName = "Echo", LastName = "Foxtrott"},
                new Person {FirstName = "Golf", LastName = "Hotel"},
                new Person {FirstName = "India", LastName = "Juliet"}
            };
            var toBeReplaced = new[]
            {
                new Person {FirstName = "Alpha", LastName = "Bravo"},
                new Person {FirstName = "Echo", LastName = "Foxtrott"},
                new Person {FirstName = "India", LastName = "Juliet"}
            };

            Person replace(Person p) => new Person
            {
                FirstName = p.FirstName.ToLowerInvariant(),
                LastName = p.LastName.ToUpperInvariant()
            };

            input.Replace(toBeReplaced, replace, Person.Comparer).ToArray().Should().Equal(
                new Person {FirstName = "alpha", LastName = "BRAVO"},
                new Person {FirstName = "Charlie", LastName = "Delta"},
                new Person {FirstName = "echo", LastName = "FOXTROTT"},
                new Person {FirstName = "Golf", LastName = "Hotel"},
                new Person {FirstName = "india", LastName = "JULIET"});
        }
    }
}