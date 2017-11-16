using System.Windows;

namespace SimulationView.Model
{
    public static class EntityExtensions
    {
        public static Entity At(this Entity self, Vector position)
        {
            self.Position = position;
            return self;
        }
    }
}