using System;

namespace ModernRonin.Standard
{
    public static class Null
    {
        public static Action<T> Action<T>() => _ => { };
    }
}