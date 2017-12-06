namespace ModernRonin.Terrarium.Logic.Transformations {
    public interface ISimulationStateTransformer
    {
        int Priority { get; }
        ISimulationState Transform(ISimulationState state);
    }
}