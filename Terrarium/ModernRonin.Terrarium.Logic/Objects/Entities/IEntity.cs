﻿namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public interface IEntity
    {
        IEntityState State { get; }
        Genome Genome { get; }
        Entity WithState(IEntityState state);
        Entity WithGenome(Genome genome);
    }
}