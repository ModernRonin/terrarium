using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic
{
    public static class Defaults
    {
        public static IEntityState Snake => new EntityState(new List<Part>
        {
            new Part(PartKind.Absorber, new Vector2D(-1, 0)),
            new Part(PartKind.Absorber, new Vector2D(-2, 0)),
            new Part(PartKind.Store, new Vector2D(1, 0)),
            new Part(PartKind.Store, new Vector2D(2, 0)),
            new Part(PartKind.Core, new Vector2D())
        });
        public static IEntityState Cross => new EntityState(new List<Part>
        {
            new Part(PartKind.Core, new Vector2D()),
            new Part(PartKind.Absorber, new Vector2D(-1, 0)),
            new Part(PartKind.Absorber, new Vector2D(1, 0)),
            new Part(PartKind.Store, new Vector2D(0, -1)),
            new Part(PartKind.Store, new Vector2D(0, 1))
        });
        public static ISimulationState SimulationState
        {
            get
            {
                var size = new Vector2D(100, 100);

                return new SimulationState(new List<Entity> {CrossPlant, SnakePlant},
                    new List<EnergySource>
                    {
                        new EnergySource(new Vector2D(50f, 50f), 25f, new Vector2D(-0.01f, -0.003f))
                    },
                    size);
            }
        }
        public static Entity CrossPlant => new Entity(Cross.At(new Vector2D(10, 10)),
            new Genome(new Parameters(), new List<IInstruction>()));
        public static Entity SnakePlant => new Entity(Snake.At(new Vector2D(90, 90)),
            new Genome(new Parameters(), new List<IInstruction>()));
    }
}