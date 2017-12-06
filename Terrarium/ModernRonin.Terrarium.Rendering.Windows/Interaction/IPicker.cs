using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public interface IPicker : IUpdateable
    {
        event Action<IEnumerable<EntityState>> OnEntitiesPicked;
    }
}