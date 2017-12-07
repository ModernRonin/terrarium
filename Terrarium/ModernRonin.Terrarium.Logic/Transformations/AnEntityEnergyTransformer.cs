using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public abstract class AnEntityEnergyTransformer : AnEntityTransformer
    {
        public sealed override IEnumerable<Type> Dependencies => typeof(EntityResetTickEnergyTransformer)
            .AsEnumerable().Concat(AdditionalDependencies);
        protected virtual IEnumerable<Type> AdditionalDependencies => Null.Enumerable<Type>();
        protected static Entity Subtract(Entity entity, float energy) =>
            entity.WithState(entity.State.SubtractTickEnergy(energy));
        protected static Entity Add(Entity entity, float energy) =>
            entity.WithState(entity.State.AddTickEnergy(energy));
    }
}