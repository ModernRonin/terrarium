namespace ModernRonin.Terrarium.Logic.Transformations {
    public abstract class ASimulationStateTransformer<T> : ISimulationStateTransformer
    {
        public ISimulationState Transform(ISimulationState state)
        {
            var old = Get(state);
            var nu = Transform(old, state);
            return Set(state, nu);
        }
        public virtual int Priority => 0;
        protected abstract T Transform(T old, ISimulationState state);
        protected abstract T Get(ISimulationState state);
        protected abstract ISimulationState Set(ISimulationState state, T property);
    }
}