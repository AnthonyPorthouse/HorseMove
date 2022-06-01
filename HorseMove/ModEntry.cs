using GenericModConfigMenu;
using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace HorseMove
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        public ModConfig Config;
        
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();            
           
            var harmony = new Harmony(this.ModManifest.UniqueID);
            HorsePatches.Initialize(harmony, this.Monitor, helper, Config);

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            // add some config options
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Movement Settings"
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Wander when outside of Farm",
                tooltip: () => "Toggles whether the horse will move about on its own when you're away from the farm.",
                getValue: () => this.Config.wanderOutsideOfFarm,
                setValue: value => this.Config.wanderOutsideOfFarm = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Wander when weather is bad",
                tooltip: () => "Toggles whether the horse will move about on its own when there is rain or snow.",
                getValue: () => this.Config.wanderIfRaining,
                setValue: value => this.Config.wanderIfRaining = value
            );
            configMenu.AddTextOption(
                mod: this.ModManifest,
                name: () => "Wander Frequency",
                tooltip: () => "Sets the frequency in which the horse will move about on its own",
                getValue: () => this.Config.wanderFrequency,
                setValue: value => this.Config.wanderFrequency = value,
                allowedValues: new string[] { "comeback", "veryfrequent", "frequent", "infrequent" }
            );
            configMenu.AddTextOption(
                mod: this.ModManifest,
                name: () => "Wander Duration",
                tooltip: () => "Sets the length for how long the horse will move when it does wander",
                getValue: () => this.Config.wanderLength,
                setValue: value => this.Config.wanderLength = value,
                allowedValues: new string[] { "long", "medium", "short" }
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Debugging"
            );
            configMenu.AddBoolOption(
               mod: this.ModManifest,
               name: () => "Verbose Logging",
               getValue: () => this.Config.verboseLogging,
               setValue: value => this.Config.verboseLogging = value
           );
        }
    }
}