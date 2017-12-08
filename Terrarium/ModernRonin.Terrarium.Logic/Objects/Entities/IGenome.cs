using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Objects.Entities {
    public interface IGenome {
        Parameters Parameters { get; }
        WrapAroundIndexableImmutableArray<IInstruction> Instructions { get; }
    }
}