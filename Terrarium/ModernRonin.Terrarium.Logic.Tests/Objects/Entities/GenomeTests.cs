using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities
{
    [TestFixture]
    public class GenomeTests
    {
        [Test]
        public void Constructor_Sets_Instructions()
        {
            var instructions = new[]
                {Substitute.For<IInstruction>(), Substitute.For<IInstruction>(), Substitute.For<IInstruction>()};

            new Genome(new Parameters(), instructions).Instructions.Should().Equal(instructions);
        }
        [Test]
        public void Constructor_Sets_Parameters()
        {
            var parameters = new Parameters();

            new Genome(parameters, new IInstruction[0]).Parameters.Should().BeSameAs(parameters);
        }
    }
}