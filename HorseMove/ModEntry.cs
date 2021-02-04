using Harmony;
using StardewModdingAPI;

namespace HorseMove
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private ModConfig config;
        
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            config = helper.ReadConfig<ModConfig>();
            
            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);
            
            HorsePatches.Initialize(harmony, this.Monitor, config);
        }
    }
}