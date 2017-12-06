using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class DependentExtensionsTests
    {
        abstract class ATestable : IDependent
        {
            protected ATestable(Type[] dependencies) => Dependencies = dependencies;
            public IEnumerable<Type> Dependencies { get; }
        }

        class Alpha : ATestable
        {
            public Alpha(params Type[] dependencies) : base(dependencies) { }
        }

        class Bravo : ATestable
        {
            public Bravo(params Type[] dependencies) : base(dependencies) { }
        }

        class Charlie : ATestable
        {
            public Charlie(params Type[] dependencies) : base(dependencies) { }
        }

        class Delta : ATestable
        {
            public Delta(params Type[] dependencies) : base(dependencies) { }
        }

        [Test]
        public void SortByDependencies_Returns_Elements_In_Order_Of_Dependencies()
        {
            var alpha = new Alpha(typeof(Bravo), typeof(Charlie));
            var bravo = new Bravo();
            var charlie = new Charlie(typeof(Bravo), typeof(Delta));
            var delta = new Delta(typeof(Bravo));

            var unsorted = new ATestable[] {alpha, bravo, charlie, delta};

            unsorted.SortByDependencies().Should().Equal(bravo, delta, charlie, alpha);
        }
        [Test]
        public void SortByDependencies_Throws_ArgumentException_If_Circular_Dependencies()
        {
            var alpha = new Alpha(typeof(Bravo), typeof(Charlie));
            var bravo = new Bravo();
            var charlie = new Charlie(typeof(Bravo), typeof(Delta));
            var delta = new Delta(typeof(Alpha));

            var unsorted = new ATestable[] {alpha, bravo, charlie, delta};

            Action act = () => unsorted.SortByDependencies();
            act.ShouldThrow<ArgumentException>();
        }
    }
}