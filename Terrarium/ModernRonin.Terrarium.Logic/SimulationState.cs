using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct SimulationState : ISimulationState
    {
        public SimulationState(
            IEnumerable<Entity> entities,
            IEnumerable<EnergySource>energySources,
            Vector2D size = new Vector2D(),
            float[,] energyDensity = null)
        {
            Size = size;
            Entities = entities;
            EnergySources = energySources;
            EnergyDensity = energyDensity ?? EnergySources.Aggregate(Size.ToEnergyDensity(), (g, s) => s.ApplyTo(g));
        }
        public static SimulationState Default
        {
            get
            {
                var size = new Vector2D(100, 100);

                return new SimulationState(
                    new List<Entity> {Entity.Cross.At(new Vector2D(10, 10)), Entity.Snake.At(new Vector2D(90, 90))},
                    new List<EnergySource> {new EnergySource(new Vector2D(50f, 50f), 25f, new Vector2D(-0.01f, -0.003f))},
                    size);
            }
        }
        public Vector2D Size { get; }
        public IEnumerable<Entity> Entities { get; }
        public float[,] EnergyDensity { get; }
        public IEnumerable<EnergySource> EnergySources { get; }
        public IEnumerable<Entity> GetEntitiesAt(Vector2D position)
        {
            return Entities.Where(e => e.AbsoluteBoundingBox.Contains(position));
        }
    }
}