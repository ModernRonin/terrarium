using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Constructor_Sets_Genome()
        {
            var genome = Substitute.For<IGenome>();
            new Entity(null, genome).Genome.Should().BeSameAs(genome);
        }
        [Test]
        public void Constructor_Sets_State()
        {
            var state = Substitute.For<IEntityState>();
            new Entity(state, null).State.Should().BeSameAs(state);
        }
        [Test]
        public void CurrentInstruction_Return_The_Instruction_At_CurrentInstructionIndex()
        {
            var state = Substitute.For<IEntityState>();
            state.CurrentInstructionIndex.Returns(2);
            var genome = Substitute.For<IGenome>();
            var instructions = new[]
            {
                Substitute.For<IInstruction>(), Substitute.For<IInstruction>(), Substitute.For<IInstruction>(),
                Substitute.For<IInstruction>()
            };
            genome.Instructions.Returns(new WrapAroundIndexableImmutableArray<IInstruction>(instructions));

            var underTest = new Entity(state, genome);
            underTest.CurrentInstruction.Should().BeSameAs(instructions[2]);
        }
        [Test]
        public void WithGenome_DoesNotChange_State()
        {
            var state = Substitute.For<IEntityState>();
            var genome = Substitute.For<IGenome>();
            new Entity(state, null).WithGenome(genome).State.Should().BeSameAs(state);
        }
        [Test]
        public void WithGenome_Sets_Genome()
        {
            var genome = Substitute.For<IGenome>();
            new Entity(null, null).WithGenome(genome).Genome.Should().BeSameAs(genome);
        }
        [Test]
        public void WithState_DoesNotChange_Genome()
        {
            var state = Substitute.For<IEntityState>();
            var genome = Substitute.For<IGenome>();
            new Entity(null, genome).WithState(state).Genome.Should().BeSameAs(genome);
        }
        [Test]
        public void WithState_Sets_State()
        {
            var state = Substitute.For<IEntityState>();
            new Entity(null, null).WithState(state).State.Should().BeSameAs(state);
        }
    }
}