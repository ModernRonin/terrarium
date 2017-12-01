using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_WidthOne()
        {
            var underTest= CreateSinglePartEntity();
            underTest.LocalBoundingBox.Width.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_HeightOne()
        {
            var underTest= CreateSinglePartEntity();
            underTest.LocalBoundingBox.Height.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_MinCornerZeroZero()
        {
            var underTest= CreateSinglePartEntity();
            underTest.LocalBoundingBox.MinCorner.OughtTo().Approximate(0, 0);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_One_Part_Only_Has_MaxCornerOneOne()
        {
            var underTest= CreateSinglePartEntity();
            underTest.LocalBoundingBox.MaxCorner.OughtTo().Approximate(1, 1);
        }
        static Entity CreateSinglePartEntity()
        {
            return new Entity(new []{new Part(PartKind.Core, Vector2D.Zero)});
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Horizontally_Aligned_Has_HeightOne()
        {
            var underTest= CreateTwoHorizontalPartsEntity();
            underTest.LocalBoundingBox.Height.OughtTo().Approximate(1);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Horizontally_Aligned_Has_WidthTwo()
        {
            var underTest= CreateTwoHorizontalPartsEntity();
            underTest.LocalBoundingBox.Width.OughtTo().Approximate(2);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Vertically_Aligned_Has_HeightTwo()
        {
            var underTest= CreateTwoVerticalPartsEntity();
            underTest.LocalBoundingBox.Height.OughtTo().Approximate(2);
        }
        [Test]
        public void LocalBoundingBox_Of_Entity_With_Two_Parts_Vertically_Aligned_Has_WidthOne()
        {
            var underTest= CreateTwoVerticalPartsEntity();
            underTest.LocalBoundingBox.Width.OughtTo().Approximate(1);
        }
        static Entity CreateTwoHorizontalPartsEntity()
        {
            return new Entity(new []
            {
                new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, new Vector2D(1, 0)),     
            });
        }
        static Entity CreateTwoVerticalPartsEntity()
        {
            return new Entity(new []
            {
                new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, new Vector2D(0, 1)),     
            });
        }
        [Test]
        public void LocalBoundingBox_Of_SwissCrossShaped_Entity_Has_Height3()
        {
            Entity.Cross.LocalBoundingBox.Height.OughtTo().Approximate(3);
        }
        [Test]
        public void LocalBoundingBox_Of_SwissCrossShaped_Entity_Has_Width3()
        {
            Entity.Cross.LocalBoundingBox.Width.OughtTo().Approximate(3);
        }
        [Test]
        public void LocalBoundingBox_Of_3HorizontalParts_Entity_Has_Width3()
        {
            Entity.Snake.LocalBoundingBox.Width.OughtTo().Approximate(3);
        }
        [Test]
        public void LocalBoundingBox_Of_3HorizontalParts_Entity_Has_Height1()
        {
            Entity.Snake.LocalBoundingBox.Height.OughtTo().Approximate(1);
        }
        [Test]
        public void AbsoluteBoundingBox()
        {
            Entity.Cross.At(new Vector2D(10, 20)).AbsoluteBoundingBox.OughtTo().Approximate(9, 19, 11, 21);
        }
        [Test]
        public void Code_Is_Different_If_Parts_Are()
        {
            Entity.Cross.Code.Should().NotBe(Entity.Snake.Code);
        }
        [Test]
        public void Code_Is_Equal_If_Parts_Are()
        {
            var cross1 = Entity.Cross.At(new Vector2D(2f, 3f));
            var cross2 = Entity.Cross.At(new Vector2D(4f, 5f));
            cross1.Code.Should().Be(cross2.Code);
        }
    }
}