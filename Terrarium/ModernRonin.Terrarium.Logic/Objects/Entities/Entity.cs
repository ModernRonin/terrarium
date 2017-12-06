namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Entity
    {
        public Entity(IEntityState state, Genome genome)
        {
            State = state;
            Genome = genome;
        }
        public IEntityState State { get; }
        public Genome Genome { get; }
        public Entity WithState(IEntityState state) => new Entity(state, Genome);
        public Entity WithGenome(Genome genome) => new Entity(State, genome);
        public override string ToString()
        {
            // TODO: add genome
            return State.Code;
        }
    }
}