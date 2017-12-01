﻿using FluentAssertions;
using ModernRonin.Standard;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class PartTests
    {
        [Test]
        public void Code()
        {
            var underTest= new Part(PartKind.Absorber, new Vector2D(3.141f, 2.71f));
            underTest.Code.Should().Be("Absorber!3.141!2.71");
        }
    }
}