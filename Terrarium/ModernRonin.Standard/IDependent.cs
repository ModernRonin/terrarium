using System;
using System.Collections.Generic;

namespace ModernRonin.Standard
{
    public interface IDependent
    {
        IEnumerable<Type> Dependencies { get; }
    }
}