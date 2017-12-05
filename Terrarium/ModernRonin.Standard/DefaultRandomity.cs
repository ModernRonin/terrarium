using System;

namespace ModernRonin.Standard {
    public class DefaultRandomity : ARandomity
    {
        readonly Random mRandom;
        public DefaultRandomity(int seed) => mRandom = new Random(seed);
        public override double Double() => mRandom.NextDouble();
    }
}