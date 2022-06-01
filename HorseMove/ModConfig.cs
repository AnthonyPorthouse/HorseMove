using System;
namespace HorseMove
{
    public class ModConfig
    {
        public string wanderFrequency { get; set; }
        public string wanderLength { get; set; }
        public bool wanderOutsideOfFarm { get; set; }
        public bool wanderIfRaining { get; set; }
        public bool verboseLogging { get; set; }

        public ModConfig()
        {
            wanderFrequency = "frequent";
            wanderLength = "short";
            wanderOutsideOfFarm = false;
            wanderIfRaining = false;
            verboseLogging = false;
        }

        public float GetWanderFrequency()
        {
            switch (wanderFrequency)
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
            switch (wanderLength)
            {
                case "long":
                    return new Tuple<int, int>(90, 300);

                case "medium":
                    return new Tuple<int, int>(60, 120);

                default:
                case "short":
                    return new Tuple<int, int>(30, 90);
            }
        }
    }
}