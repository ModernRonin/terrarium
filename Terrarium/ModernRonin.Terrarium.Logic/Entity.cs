namespace ModernRonin.Terrarium.Logic
{
    public class Entity
    {
        public Entity(EntityState state, Genome genome)
        {
            State = state;
            Genome = genome;
        }
        public EntityState State { get; }
        public Genome Genome { get; }
        public Entity WithState(EntityState state) => new Entity(state, Genome);
        public Entity WithGenome(Genome genome) => new Entity(State, genome);
        public override string ToString()
        {
            // TODO: add genome
            return State.Code;
        }
    }
}