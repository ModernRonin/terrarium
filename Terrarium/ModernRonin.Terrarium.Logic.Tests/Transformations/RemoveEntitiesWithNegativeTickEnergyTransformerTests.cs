using System.Collections.Generic;
using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class RemoveEntitiesWithNegativeTickEnergyTransformerTests
    {
        [Test]
        public void Removes_Entities_With_Negative_TickEnergy()
        {
            var alpha = Substitute.For<IEntity>();
            var bravo = Substitute.For<IEntity>();
            var charlie = Substitute.For<IEntity>();
            alpha.State.TickEnergy.Returns(11);
            bravo.State.TickEnergy.Returns(-3);
            charlie.State.TickEnergy.Returns(0);
            var state = Substitute.For<ISimulationState>();
            state.Entities.Returns(new[] {alpha, bravo, charlie});
            state.WhenForAnyArgs(s => s.WithEntities(null)).Do(ci =>
            {
                ci.Arg<IEnumerable<IEntity>>().Should().OnlyContain(e => e == alpha || e == charlie);
            });

            var underTest = new RemoveEntitiesWithNegativeTickEnergyTransformer();
            underTest.Transform(state);

            state.ReceivedWithAnyArgs().WithEntities(null);
        }
    }
}