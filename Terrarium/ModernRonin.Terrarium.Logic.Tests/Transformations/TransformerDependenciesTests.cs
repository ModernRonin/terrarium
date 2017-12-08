using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Transformations;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using ModernRonin.Terrarium.Logic.Transformations.Framework;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    // ReSharper disable once TestFileNameWarning
    public class TransformerDependenciesTests
    {
        // @formatter:off
        readonly IDictionary<Type, ISimulationStateTransformerWithDependencies> mTransformers =
            new ISimulationStateTransformerWithDependencies[]
            {
                new EnergySourceMovingTransformer(),
                new EntityCurrentInstructionCostTransformer(null),
                new EntityEnergyAbsorptionTransformer(),
                new EntityEnergyStoreTransformer(null),
                new EntityPartsEnergyCostTransformer(null),
                new EntityResetTickEnergyTransformer(),
                new RemoveEntitiesWithNegativeTickEnergyTransformer(),
                new EntityExecuteCurrentInstructionTransformer(Null.Enumerable<IInstructionExecutor>()), 
            }.ToDictionary(t => t.GetType());
        static IEnumerable<Dependency> TestCases
        {
            get
            {
                yield return DependencyOf<EnergySourceMovingTransformer>().OnNothing();
                yield return DependencyOf<EntityResetTickEnergyTransformer>().OnNothing();
                yield return DependencyOf<EntityCurrentInstructionCostTransformer>().On<EntityResetTickEnergyTransformer>();
                yield return DependencyOf<EntityEnergyAbsorptionTransformer>().On<EntityResetTickEnergyTransformer>();
                yield return DependencyOf<EntityEnergyStoreTransformer>().On<EntityResetTickEnergyTransformer>();
                yield return DependencyOf<EntityEnergyStoreTransformer>().On<EntityEnergyAbsorptionTransformer>();
                yield return DependencyOf<EntityEnergyStoreTransformer>().On<EntityCurrentInstructionCostTransformer>();
                yield return DependencyOf<EntityEnergyStoreTransformer>().On<EntityPartsEnergyCostTransformer>();
                yield return DependencyOf<EntityPartsEnergyCostTransformer>().On<EntityResetTickEnergyTransformer>();
                yield return DependencyOf<RemoveEntitiesWithNegativeTickEnergyTransformer>().On<EntityEnergyStoreTransformer>();
                yield return DependencyOf<EntityExecuteCurrentInstructionTransformer>()
                    .On<RemoveEntitiesWithNegativeTickEnergyTransformer>();
            }
        }

        // @formatter:on
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
        [Test]
        public void DependsOn([ValueSource(nameof(TestCases))] Dependency dependency)
        {
            var instance = mTransformers[dependency.Depender];
            if (null == dependency.Dependee) instance.Dependencies.Should().BeEmpty();
            else instance.Dependencies.Should().Contain(dependency.Dependee);
        }
    }
}