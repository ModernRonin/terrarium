using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public interface IPicker : IUpdateable
    {
        event Action<IEnumerable<Entity>> OnEntitiesPicked;
    }
}