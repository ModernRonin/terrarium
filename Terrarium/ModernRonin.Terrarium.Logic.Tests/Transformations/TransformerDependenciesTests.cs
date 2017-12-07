using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ModernRonin.Terrarium.Logic.Transformations;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    // ReSharper disable once TestFileNameWarning
    public class TransformerDependenciesTests
    {
        readonly IDictionary<Type, ISimulationStateTransformerWithDependencies> mTransformers =
            new ISimulationStateTransformerWithDependencies[]
            {
                new EnergySourceMovingTransformer(), new EntityCurrentInstructionCostTransformer(null),
                new EntityEnergyAbsorptionTransformer(), new EntityPartsEnergyCostTransformer(null),
                new EntityEnergyStoreTransformer(), new EntityResetTickEnergyTransformer()
            }.ToDictionary(t => t.GetType());

        public class Dependency
        {
            public Type Depender { get; set; }
            public Type Dependee { get; set; }
            public Dependency OnNothing()
            {
                Dependee = null;
                return this;
            }
            public Dependency On<T>() where T : ISimulationStateTransformerWithDependencies
            {
                Dependee = typeof(T);
                return this;
            }
            public override string ToString()
            {
                var dependee = Dependee == null ? "nothing" : Dependee.Name;
                return $"{Depender.Name} on {dependee}";
            }
        }

        static Dependency DependencyOf<T>() where T : ISimulationStateTransformerWithDependencies =>
            new Dependency {Depender = typeof(T)};
        static IEnumerable<Dependency> TestCases
        {
            get
            {
                yield return DependencyOf<EnergySourceMovingTransformer>().OnNothing();
                yield return DependencyOf<EntityResetTickEnergyTransformer>().OnNothing();
                yield return DependencyOf<EntityCurrentInstructionCostTransformer>()
                    .On<EntityResetTickEnergyTransformer>();
            }
        }
        [Test]
        public void DependsOn([ValueSource(nameof(TestCases))] Dependency dependency)
        {
            var instance = mTransformers[dependency.Depender];
            if (null == dependency.Dependee) instance.Dependencies.Should().BeEmpty();
            else instance.Dependencies.Should().Contain(dependency.Dependee);
        }
    }
}