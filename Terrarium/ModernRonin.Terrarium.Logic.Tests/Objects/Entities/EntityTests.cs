using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Constructor_Sets_State()
        {
            var state = Substitute.For<IEntityState>();
            new Entity(state, null).State.Should().BeSameAs(state);
        }
        [Test]
        public void Constructor_Sets_Genome()
        {
            
        }
    }
}

