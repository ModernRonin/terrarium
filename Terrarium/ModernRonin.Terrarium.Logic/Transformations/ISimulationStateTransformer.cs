namespace ModernRonin.Terrarium.Logic.Transformations
{
    public interface ISimulationStateTransformer
    {
        ISimulationState Transform(ISimulationState state);
    }
}