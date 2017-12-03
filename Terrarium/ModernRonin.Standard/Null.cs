using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.Standard
{
    public static class Null
    {
        public static Action<T> Action<T>() => _ => { };
        public static IEnumerable<T> Enumerable<T>() => new T[0];
    }
}