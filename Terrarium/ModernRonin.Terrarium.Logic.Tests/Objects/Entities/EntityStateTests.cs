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
            new EntityState(sCorePart.AsEnumerable(), new Vector2D(13f, 17f), 23f, 29f, 19);
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

        public class BuilderMethodSpec
        {
            public Expression<Func<EntityState, IEntityState>> Method { get; set; }
            public Expression<Func<IEntityState, object>> Getter { get; set; }
            public override string ToString() => $"{Method} / {Getter}";
        }

        static IEnumerable<BuilderMethodSpec> BuilderMethodCalls
        {
            get
            {
                yield return new BuilderMethodSpec {Method = e => e.AddTickEnergy(13f), Getter = e => e.TickEnergy};
                yield return new BuilderMethodSpec
                {
                    Method = e => e.SubtractTickEnergy(13f),
                    Getter = e => e.TickEnergy
                };
                yield return new BuilderMethodSpec {Method = e => e.ResetTickEnergy(), Getter = e => e.TickEnergy};
                yield return new BuilderMethodSpec
                {
                    Method = e => e.WithParts(new[] {sCorePart, sCorePart}),
                    Getter = e => e.Parts
                };
                yield return new BuilderMethodSpec
                {
                    Method = e => e.WithCurrentInstructionIndex(129),
                    Getter = e => e.CurrentInstructionIndex
                };
                yield return new BuilderMethodSpec {Method = e => e.At(new Vector2D(-2, -3)), Getter = e => e.Position};
                yield return new BuilderMethodSpec {Method = e => e.AddStoredEnergy(31f), Getter = e => e.StoredEnergy};
                yield return new BuilderMethodSpec
                {
                    Method = e => e.SubtractStoredEnergy(31f),
                    Getter = e => e.StoredEnergy
                };
            }
        }
        [Test]
        public void AbsoluteBoundingBox()
        {
            Defaults.Cross.At(new Vector2D(10, 20)).AbsoluteBoundingBox.OughtTo().Approximate(9, 19, 12, 22);
        }
        [Test]
        public void AddStoredEnergy_Adds_To_TickEnergy()
        {
            mFullyConstructed.AddStoredEnergy(13f).StoredEnergy.OughtTo().Approximate(42f);
        }
        [Test]
        public void AddTickEnergy_Adds_To_TickEnergy()
        {
            mFullyConstructed.AddTickEnergy(13f).TickEnergy.OughtTo().Approximate(36f);
        }
        [Test]
        public void At_Sets_Position()
        {
            mFullyConstructed.At(new Vector2D(-1f, -2f)).Position.Should().Be(new Vector2D(-1f, -2f));
        }
        [Test]
        public void BuilderMethods_Return_Different_Instances(
            [ValueSource(nameof(BuilderMethodCalls))] BuilderMethodSpec builderMethodSpec)
        {
            builderMethodSpec.Method.Compile()(mFullyConstructed).Should().NotBeSameAs(mFullyConstructed);
        }
        [Test]
        public void BuilderMethods_Return_Equivalent_Except_For_Target_Property(
            [ValueSource(nameof(BuilderMethodCalls))] BuilderMethodSpec builderMethodSpec)
        {
            builderMethodSpec.Method.Compile()(mFullyConstructed).ShouldBeEquivalentTo(mFullyConstructed,
                cfg => BuilderStandardEquivalency(cfg).Excluding(builderMethodSpec.Getter));
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
        public void ResetTickEnergy_Sets_TickEnergy_To_Zero()
        {
            mFullyConstructed.ResetTickEnergy().TickEnergy.OughtTo().Approximate(0f);
        }
        [Test]
        public void SubtractStoredEnergy_Subtracts_From_TickEnergy()
        {
            mFullyConstructed.SubtractStoredEnergy(13f).StoredEnergy.OughtTo().Approximate(16f);
        }
        [Test]
        public void SubtractTickEnergy_Subtracts_From_TickEnergy()
        {
            mFullyConstructed.SubtractTickEnergy(13f).TickEnergy.OughtTo().Approximate(10f);
        }
        [Test]
        public void WithCurrentInstructionIndex_Sets_CurrentInstructionIndex()
        {
            mFullyConstructed.WithCurrentInstructionIndex(29).CurrentInstructionIndex.Should().Be(29);
        }
        [Test]
        public void WithParts_Sets_Parts()
        {
            var parts = new Part[2];
            new EntityState(Null.Enumerable<Part>()).WithParts(parts).Parts.Should().BeSameAs(parts);
        }
    }
}