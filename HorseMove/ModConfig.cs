using System;
namespace HorseMove
{
    public class ModConfig
    {
        public string WanderFrequency { get; set; } = "frequent";
        public WanderRange WanderDuration { get; set; } = new WanderRange(0.5f, 2f);
        public bool WanderOutsideOfFarm { get; set; } = false;
        public bool WanderIfRaining { get; set; } = false;
        public bool VerboseLogging { get; set; } = false;

        public float GetWanderFrequency()
        {
            switch (WanderFrequency)
            {
                case "comeback":
                    return 1f;
                case "veryfrequent":
                    return 0.02f;

                case "frequent":
                    return 0.002f;

                default:
                case "infrequent":
                    return 0.0002f;
            }
        }

        public Tuple<int, int> GetWanderRange()
        {
            var min = MathF.Min(WanderDuration.Min, WanderDuration.Max);
            var max = MathF.Max(WanderDuration.Min, WanderDuration.Max);

            return new Tuple<int, int>((int)(min * 60), (int)(max * 60));
        }

        public class WanderRange
        {
            public float Min { get; set; }
            public float Max { get; set; }

            public WanderRange(float min, float max)
            {
                Min = min;
                Max = max;
            }
        }
    }
}