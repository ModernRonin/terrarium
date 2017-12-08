using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public interface IEntity
    {
        IEntityState State { get; }
        IGenome Genome { get; }
        Entity WithState(IEntityState state);
        Entity WithGenome(IGenome genome);
        IInstruction CurrentInstruction { get; }
    }
}