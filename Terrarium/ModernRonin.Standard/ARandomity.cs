using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.Standard
{
    public abstract class ARandomity : IRandomity
    {
        public abstract double Double();
        public float Float() => (float) Double();
        public int Integer(int exclusiveMaximum) => (int) (exclusiveMaximum * Double());
        public int Integer(int inclusiveMinimum, int exclusiveMaximum) => Integer(exclusiveMaximum) + inclusiveMinimum;
        public bool Boolean() => Double() >= 0.5;
        public T ElementOf<T>(IList<T> list)
        {
            var index = Integer(list.Count);
            return list[index];
        }
        public T ElementOf<T>(IEnumerable<T> enumerable) => ElementOf(enumerable.ToList());
        public bool IsSmallerThan(float rhs) => Float() < rhs;
        public bool IsSmallerThan(double rhs) => Double() < rhs;
        public T EnumValue<T>()
        {
            var type = typeof(T);
            if (!typeof(Enum).IsAssignableFrom(type))
                throw new ArgumentException($"The type parameter {nameof(T)} must be an enum type.");
            var values = Enum.GetValues(type).Cast<T>().ToArray();
            return ElementOf(values);
        }
    }
}