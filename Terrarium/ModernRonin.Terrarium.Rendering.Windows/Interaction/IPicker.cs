using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Interaction
{
    public interface IPicker
    {
        event Action<IEnumerable<Entity>> OnEntitiesPicked;
        void Update();
    }
}