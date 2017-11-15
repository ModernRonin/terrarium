namespace SimulationView.Model
{
    public static class EntityExtensions
    {
        public static Entity At(this Entity entity, Vector2D position)
        {
            entity.Position = position;
            return entity;
        }
    }
}