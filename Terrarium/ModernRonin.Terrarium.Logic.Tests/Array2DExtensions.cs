using System;
using System.Collections.Generic;
using FluentAssertions;

namespace ModernRonin.Terrarium.Logic.Tests
{
    public static class Array2DExtensions
    {
        public static void Set<T>(this T[,] self, T value)
        {
            Do(self, (x, y) => self[x, y] = value);
        }
        public static void Do<T>(this T[,] self, Action<int, int> what)
        {
            for (var x = 0; x < self.GetLength(0); ++x) for (var y = 0; y < self.GetLength(1); ++y) what(x, y);
        }
        public static IEnumerable<T> ToEnumerable<T>(this T[,] self)
        {
            for (var x = 0; x < self.GetLength(0); ++x)
            for (var y = 0; y < self.GetLength(1); ++y) yield return self[x, y];
        }
        public static void ShouldAllBe<T>(this T[,] self, T expected)
        {
            foreach (var element in self.ToEnumerable()) element.Should().Be(expected);
        }
    }
}