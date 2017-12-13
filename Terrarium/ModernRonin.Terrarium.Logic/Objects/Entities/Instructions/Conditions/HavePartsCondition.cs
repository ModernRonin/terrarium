using System;
using System.Linq;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions
{
    public class HavePartsCondition : ACondition
    {
        public HavePartsCondition(PartKind of, Multiplicity count)
        {
            Of = of;
            Count = count;
        }
        public PartKind Of { get; }
        public Multiplicity Count { get; }
        protected override bool IsFullfilled(IEntityState state, Parameters parameters)
        {
            var actualCount = state.Parts.OfKind(Of).Count();
            switch (Count)
            {
                case Multiplicity.No:
                    return 0 == actualCount;
                case Multiplicity.Few:
                    return 0 < actualCount && actualCount <= parameters.FewPartsThreshold;
                case Multiplicity.Many:
                    return 0 < actualCount && actualCount >= parameters.ManyPartsThreshold;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}