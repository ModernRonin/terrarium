using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityCurrentInstructionCostTransformerTests
    {
        [Test]
        public void Deducts_Cost_For_CurrentInstruction()
        {
            var instructionAlpha = Substitute.For<IInstruction>();
            var instructionBravo = Substitute.For<IInstruction>();
            var configuration = Substitute.For<IEnergyCostConfiguration>();
            configuration.GetEnergyCostForInstruction(instructionAlpha).Returns(13f);
            var entity = new Entity(new EntityState(Null.Enumerable<Part>()),
                new Genome(new Parameters(), new List<IInstruction> {instructionAlpha, instructionBravo}));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest = new EntityCurrentInstructionCostTransformer(configuration);
            var changed = underTest.Transform(state).Entities.Single();

            changed.State.TickEnergy.OughtTo().Approximate(-13f);
        }
    }
}