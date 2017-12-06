using System;
using System.Collections.Concurrent;
using System.Linq;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations;
using MoreLinq;

namespace ModernRonin.Terrarium.Logic
{
    public class Configuration : IEnergyCostConfiguration
    {
        readonly ConcurrentDictionary<PartKind, float> mPartKindCosts = new ConcurrentDictionary<PartKind, float>();
        public Configuration()
        {
            Enum.GetValues(typeof(PartKind)).Cast<PartKind>().ForEach(k => mPartKindCosts[k] = 1);
        }
        public float GetEnergyCostForPartKind(PartKind kind) => mPartKindCosts[kind];
        public float GetEnergyCostForInstruction(IInstruction instruction)
        {
            switch (instruction)
            {
                case GrowInstruction _:
                    return 10;
                case JumpIfInstruction _:
                    return 0.2f;
                case JumpInstruction _:
                    return 0.1f;
                case RotateThrusterInstruction _:
                    return 0.75f;
                case PulseThrusterInstruction _:
                    return 1;
            }
            throw new NotImplementedException();
        }
        public float SetEnergyCostForPartKind(PartKind kind, float cost) => mPartKindCosts[kind] = cost;
    }
}