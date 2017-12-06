namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Parameters
    {
        public Parameters(
            float hungryThreshold= 0,
            float fatThreshold = 0,
            float movingQuicklyThreshold = 0,
            float movingSlowlyThreshold = 0,
            int manyPartsThreshold = 0,
            int fewPartsThreshold = 0)
        {
            HungryThreshold = hungryThreshold;
            FatThreshold = fatThreshold;
            MovingQuicklyThreshold = movingQuicklyThreshold;
            MovingSlowlyThreshold = movingSlowlyThreshold;
            ManyPartsThreshold = manyPartsThreshold;
            FewPartsThreshold = fewPartsThreshold;
        }
        public float HungryThreshold { get; }
        public float FatThreshold { get; }
        public float MovingQuicklyThreshold { get; }
        public float MovingSlowlyThreshold { get; }
        public int ManyPartsThreshold { get; }
        public int FewPartsThreshold { get; }
    }
}