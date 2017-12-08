namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions
{
    public interface IInstruction
    {
    }
    public interface IEntityChangingInstruction : IInstruction { }
    public interface ISimulationChangingInstruction : IInstruction { }
}