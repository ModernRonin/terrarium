namespace ModernRonin.Terrarium.Logic.Transformations.Framework
{
    public interface ISimulationStateTransformer
    {
        ISimulationState Transform(ISimulationState state);
    }
}