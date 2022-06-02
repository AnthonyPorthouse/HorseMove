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
            I18n.Init(helper.Translation);
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
                text: I18n.Config_Horsemove_MovementSettings
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: I18n.Config_Horsemove_WanderOffFarm_Name,
                tooltip: I18n.Config_Horsemove_WanderOffFarm_Tooltip,
                getValue: () => this.Config.WanderOutsideOfFarm,
                setValue: value => this.Config.WanderOutsideOfFarm = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: I18n.Config_Horsemove_WanderIfRaining_Name,
                tooltip: I18n.Config_Horsemove_WanderIfRaining_Tooltip,
                getValue: () => this.Config.WanderIfRaining,
                setValue: value => this.Config.WanderIfRaining = value
            );
            configMenu.AddTextOption(
                mod: this.ModManifest,
                name: I18n.Config_Horsemove_WanderFrequency_Name,
                tooltip:I18n.Config_Horsemove_WanderFrequency_Tooltip,
                getValue: () => this.Config.WanderFrequency,
                setValue: value => this.Config.WanderFrequency = value,
                allowedValues: new string[] { "comeback", "veryfrequent", "frequent", "infrequent" },
                formatAllowedValue: value => I18n.GetByKey($"config.horsemove.wander-frequency.values.{value}")

            );
            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: I18n.Config_Horsemove_WanderMinimumDuration_Name,
                tooltip: I18n.Config_Horsemove_WanderMinimumDuration_Tooltip,
                getValue: () => this.Config.WanderDuration.Min,
                setValue: value => this.Config.WanderDuration.Min = value,
                min: 0f,
                max: 10f,
                interval: 0.5f
            ); 
            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: I18n.Config_Horsemove_WanderMaximumDuration_Name,
                tooltip: I18n.Config_Horsemove_WanderMinimumDuration_Tooltip,
                getValue: () => this.Config.WanderDuration.Max,
                setValue: value => this.Config.WanderDuration.Max = value,
                min: 0f,
                max: 10f,
                interval: 0.5f
            ); 
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Debugging"
            );
            configMenu.AddBoolOption(
               mod: this.ModManifest,
               name: I18n.Config_Horsemove_VerboseLogging_Name,
               tooltip: I18n.Config_Horsemove_VerboseLogging_Tooltip,
               getValue: () => this.Config.VerboseLogging,
               setValue: value => this.Config.VerboseLogging = value
           );
        }
    }
}