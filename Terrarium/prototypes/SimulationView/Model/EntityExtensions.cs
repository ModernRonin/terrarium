using System.Windows;

namespace SimulationView.Model
{
    public static class EntityExtensions
    {
        public static Entity At(this Entity entity, Vector position)
        {
            entity.Position = position;
            return entity;
        }
    }
}