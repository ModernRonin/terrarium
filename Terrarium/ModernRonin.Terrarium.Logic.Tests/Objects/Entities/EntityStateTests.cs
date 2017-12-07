using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Equivalency;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities
{
    [TestFixture]
    public class EntityStateTests
    {
        readonly EntityState mFullyConstructed =
            new EntityState(sCorePart.AsEnumerable(), new Vector2D(13f, 17f), 23f, 19);
        static readonly Part sCorePart = new Part(PartKind.Core, Vector2D.Zero);
        static EntityState CreateSinglePartEntity() => new EntityState(new[] {new Part(PartKind.Core, Vector2D.Zero)});
        static EntityState CreateTwoHorizontalPartsEntity() => new EntityState(new[]
            {new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, new Vector2D(1, 0))});
        static EntityState CreateTwoVerticalPartsEntity() => new EntityState(new[]
            {new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, new Vector2D(0, 1))});
        static EquivalencyAssertionOptions<IEntityState> BuilderStandardEquivalency(
            EquivalencyAssertionOptions<IEntityState> cfg)
        {
            return cfg.Using<Vector2D>(ctx => ctx.Subject.Should().Be(ctx.Expectation)).WhenTypeIs<Vector2D>()
                      .Using<Rectangle2D>(ctx => ctx.Subject.Should().Be(ctx.Expectation)).WhenTypeIs<Rectangle2D>()
                      .Excluding(u => u.AbsoluteBoundingBox).Excluding(u => u.Code);
        }
        [Test]
        public void AbsoluteBoundingBox()
        {
            Defaults.Cross.At(new Vector2D(10, 20)).AbsoluteBoundingBox.OughtTo().Approximate(9, 19, 12, 22);
        }

        static IEnumerable<Expression<Func<EntityState, IEntityState>>> BuilderMethodCalls
        {
            get
            {
                yield return e => e.AddTickEnergy(13f);
                yield return e => e.SubtractTickEnergy(13f);
                yield return e => e.ResetTickEnergy();
                yield return e => e.WithParts(new[] {sCorePart, sCorePart});
                yield return e => e.WithCurrentInstructionIndex(129);
            }
        }
        [Test]
        public void BuilderMethods_Return_Different_Instances([ValueSource(nameof(BuilderMethodCalls))] Expression<Func<EntityState, IEntityState>>  builderMethod)
        {
            builderMethod.Compile()(mFullyConstructed).Should().NotBeSameAs(mFullyConstructed);
        }

        [Test]
        public void AddTickEnergy_Adds()
        {
            mFullyConstructed.AddTickEnergy(13f).TickEnergy.OughtTo().Approximate(36f);
        }
        [Test]
        public void AddTickEnergy_Returns_Equivalent_Except_For_TickEnergy()
        {
            mFullyConstructed.AddTickEnergy(13f).ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(u => u.TickEnergy));
        }
        [Test]
        public void At_Returns_Equivalent_Except_Position()
        {
            mFullyConstructed.At(Vector2D.Zero).ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(u => u.Position));
        }
        [Test]
        public void At_Sets_Position()
        {
            mFullyConstructed.At(new Vector2D(-1f, -2f)).Position.Should().Be(new Vector2D(-1f, -2f));
        }
        [Test]
        public void Code_Is_Different_If_Parts_Are()
        {
            Defaults.Cross.Code.Should().NotBe(Defaults.Snake.Code);
        }
        [Test]
        public void Code_Is_Equal_If_Parts_Are()
        {
            var cross1 = Defaults.Cross.At(new Vector2D(2f, 3f));
            var cross2 = Defaults.Cross.At(new Vector2D(4f, 5f));
            cross1.Code.Should().Be(cross2.Code);
        }
        [Test]
        public void Constructor_Sets_CurrentInstructionIndex_If_Passed()
        {
            new EntityState(Null.Enumerable<Part>(), currentInstructionIndex: 19)
                .CurrentInstructionIndex.Should().Be(19);
        }
        [Test]
        public void Constructor_Sets_CurrentInstructionIndex_To_Zero_If_Not_Passed()
        {
            new EntityState(Null.Enumerable<Part>()).CurrentInstructionIndex.Should().Be(0);
        }
        [Test]
        public void Constructor_Sets_Parts()
        {
            var parts = new List<Part>();
            new EntityState(parts).Parts.Should().BeSameAs(parts);
        }
        [Test]
        public void Constructor_Sets_Position_If_Passed()
        {
            new EntityState(Null.Enumerable<Part>(), new Vector2D(13f, 17f))
                .Position.Should().Be(new Vector2D(13f, 17f));
        }
        [Test]
        public void Constructor_Sets_Position_To_ZeroZero_If_Not_Passed()
        {
            new EntityState(Null.Enumerable<Part>()).Position.Should().Be(Vector2D.Zero);
        }
        [Test]
        public void Constructor_Sets_TickEnergy_If_Passed()
        {
            new EntityState(Null.Enumerable<Part>(), tickEnergy: 14f).TickEnergy.Should().Be(14f);
        }
        [Test]
        public void Constructor_Sets_TickEnergy_To_Zero_If_Not_Passed()
        {
            new EntityState(Null.Enumerable<Part>()).TickEnergy.Should().Be(0);
        }
        [Test]
        public void LocalBoundingBox_Of_3HorizontalParts_Entity_Has_Height1()
        {
            Defaults.Snake.LocalBoundingBox.Height.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_5HorizontalParts_Entity_Has_Width5()
        {
            Defaults.Snake.LocalBoundingBox.Width.OughtTo().Approximate(5);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_HeightOne()
        {
            var underTest = CreateSinglePartEntity();
            underTest.LocalBoundingBox.Height.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_MaxCornerOneOne()
        {
            var underTest = CreateSinglePartEntity();
            underTest.LocalBoundingBox.MaxCorner.OughtTo().Approximate(1, 1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_MinCornerZeroZero()
        {
            var underTest = CreateSinglePartEntity();
            underTest.LocalBoundingBox.MinCorner.OughtTo().Approximate(0, 0);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_WidthOne()
        {
            var underTest = CreateSinglePartEntity();
            underTest.LocalBoundingBox.Width.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Horizontally_Aligned_Has_HeightOne()
        {
            var underTest = CreateTwoHorizontalPartsEntity();
            underTest.LocalBoundingBox.Height.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Horizontally_Aligned_Has_WidthTwo()
        {
            var underTest = CreateTwoHorizontalPartsEntity();
            underTest.LocalBoundingBox.Width.OughtTo().Approximate(2);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Vertically_Aligned_Has_HeightTwo()
        {
            var underTest = CreateTwoVerticalPartsEntity();
            underTest.LocalBoundingBox.Height.OughtTo().Approximate(2);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Vertically_Aligned_Has_WidthOne()
        {
            var underTest = CreateTwoVerticalPartsEntity();
            underTest.LocalBoundingBox.Width.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_SwissCrossShaped_Entity_Has_Height3()
        {
            Defaults.Cross.LocalBoundingBox.Height.OughtTo().Approximate(3);
        }
        [Test]
        public void LocalBoundingBox_Of_SwissCrossShaped_Entity_Has_Width3()
        {
            Defaults.Cross.LocalBoundingBox.Width.OughtTo().Approximate(3);
        }
        [Test]
        public void ResetTickEnergy_Returns_Equivalent_Except_For_TickEnergy()
        {
            mFullyConstructed.ResetTickEnergy().ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(u => u.TickEnergy));
        }
        [Test]
        public void ResetTickEnergy_Sets_TickEnergy_To_Zero()
        {
            mFullyConstructed.ResetTickEnergy().TickEnergy.OughtTo().Approximate(0f);
        }
        [Test]
        public void SubtractTickEnergy_Returns_Equivalent_Except_For_TickEnergy()
        {
            mFullyConstructed.SubtractTickEnergy(14f).ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(u => u.TickEnergy));
        }
        [Test]
        public void SubtractTickEnergy_Subtracts()
        {
            mFullyConstructed.SubtractTickEnergy(13f).TickEnergy.OughtTo().Approximate(10f);
        }
        [Test]
        public void WithCurrentInstructionIndex_Returns_Equivalent_Except_CurrentInstructionIndex()
        {
            mFullyConstructed.WithCurrentInstructionIndex(29).ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(u => u.CurrentInstructionIndex));
        }
        [Test]
        public void WithCurrentInstructionIndex_Sets_CurrentInstructionIndex()
        {
            mFullyConstructed.WithCurrentInstructionIndex(29).CurrentInstructionIndex.Should().Be(29);
        }
        [Test]
        public void WithParts_Returns_Equivalent_Except_For_Parts()
        {
            mFullyConstructed.WithParts(new[] {sCorePart, sCorePart}).ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(u => u.Parts));
        }
        [Test]
        public void WithParts_Sets_Parts()
        {
            var parts = new Part[2];
            new EntityState(Null.Enumerable<Part>()).WithParts(parts).Parts.Should().BeSameAs(parts);
        }
    }
}