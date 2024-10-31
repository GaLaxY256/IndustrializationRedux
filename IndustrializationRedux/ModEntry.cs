using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using ContentPatcher;
using GenericModConfigMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Events;
using StardewValley.Extensions;
using StardewValley.GameData.Crops;
using StardewValley.GameData.Machines;
using StardewValley.GameData.Objects;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Tools;
using xTile.Tiles;
using Object = StardewValley.Object;

namespace IndustrializationRedux
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        public static ModConfig Config;
        private CompensationData compensationData;
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        }
        private void OnGameLaunched(object? sender, StardewModdingAPI.Events.GameLaunchedEventArgs e)
        {
            Config = Helper.ReadConfig<ModConfig>();
            var api = this.Helper.ModRegistry.GetApi<IContentPatcherAPI>("Pathoschild.ContentPatcher");
            api.RegisterToken(this.ModManifest, "MachinesSale", () =>
            {
                return new[] { Config.MachinesSale.ToString() };
            });
            api.RegisterToken(this.ModManifest, "UseOres", () =>
            {
                return new[] { Config.UseOres.ToString() };
            });
            api.RegisterToken(this.ModManifest, "BetterSeedMaker", () =>
            {
                return new[] { Config.BetterSeedMaker.ToString() };
            });
            api.RegisterToken(this.ModManifest, "ProgressMode", () =>
            {
                return new[] { Config.ProgressMode.ToString() };
            });
            api.RegisterToken(this.ModManifest, "HasShippedThreeHundred", () =>
            {
                return new[] { anyMonoShipped300.ToString() };
            });
            api.RegisterToken(this.ModManifest, "HasShippedLuckySeven", () =>
            {
                return new[] { anyMonoShipped300.ToString() };
            }); 
            api.RegisterToken(this.ModManifest, "EnabledAltEFurnace", () =>
            {
                return new[] { Config.EnabledAltEFurnace.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledEFurnace", () =>
            {
                return new[] { Config.EnabledEFurnace.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledAutoMiner", () =>
            {
                return new[] { Config.EnabledAutoMiner.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledCeramicKiln", () =>
            {
                return new[] { Config.EnabledCeramicKiln.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledChroFurnace", () =>
            {
                return new[] { Config.EnabledChroFurnace.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledCombustionGen", () =>
            {
                return new[] { Config.EnabledCombustionGen.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledCombustionMiner", () =>
            {
                return new[] { Config.EnabledCombustionMiner.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledGardenCloche", () =>
            {
                return new[] { Config.EnabledGardenCloche.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledGemPolisher", () =>
            {
                return new[] { Config.EnabledGemPolisher.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledHayBin", () =>
            {
                return new[] { Config.EnabledHayBin.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledHayRehydrator", () =>
            {
                return new[] { Config.EnabledHayRehydrator.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledIndustrialDistillery", () =>
            {
                return new[] { Config.EnabledIndustrialDistillery.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledJColaGenerator", () =>
            {
                return new[] { Config.EnabledJColaGenerator.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledMineralWasher", () =>
            {
                return new[] { Config.EnabledMineralWasher.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledPreservativePress", () =>
            {
                return new[] { Config.EnabledPreservativePress.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledPulverizer", () =>
            {
                return new[] { Config.EnabledPulverizer.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledRockCrusher", () =>
            {
                return new[] { Config.EnabledRockCrusher.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledSGardenCloche", () =>
            {
                return new[] { Config.EnabledSGardenCloche.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledTreeCloche", () =>
            {
                return new[] { Config.EnabledTreeCloche.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledFeSeedMaker", () =>
            {
                return new[] { Config.EnabledFeSeedMaker.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledAuSeedMaker", () =>
            {
                return new[] { Config.EnabledAuSeedMaker.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledIrSeedMaker", () =>
            {
                return new[] { Config.EnabledIrSeedMaker.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledGasSmoker", () =>
            {
                return new[] { Config.EnabledGasSmoker.ToString() };
            });

            api.RegisterToken(this.ModManifest, "EnabledFishingWell", () =>
            {
                return new[] { Config.EnabledFishingWell.ToString() };
            });

            api.RegisterToken(this.ModManifest, "NewCombustionGen", () =>
            {
                return new[] { Config.NewCombustionGen.ToString() };
            });

            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(Config)
            );

            // add some config options
            configMenu.AddPageLink(
                mod: this.ModManifest,
                pageId: "G256.EnabledMachinePage",
                text: () => "> Enable/Disable Machines <",
                tooltip: () => "Please disable progress mode when disabling ANY machines. "
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "General",
                tooltip: () => "List available config options for all machines added by this machine."
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Progress Mode",
                tooltip: () => "When checked, you have to get and buy recipes by progressing throughout the game.\nUnchecking will unlock all recipes right from the start instead.\n\nDefault unchecked so that it's same as original mod.",
                getValue: () => Config.ProgressMode,
                setValue: value => Config.ProgressMode = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Purchasable Machines",
                tooltip: () => "When checked, you can buy machines after they are unlocked.\nUnchecking will make them unpurchable instead.\n\nDefault checked.",
                getValue: () => Config.MachinesSale,
                setValue: value => Config.MachinesSale = value
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Chromium Furnace Config",
                tooltip: () => "List available config options for chromium furnace."
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Use Ores",
                tooltip: () => "When unchecked, Chromium Furnace will only use pulverized materials and not ores.\n\nDefault checked.",
                getValue: () => Config.UseOres,
                setValue: value => Config.UseOres = value
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Combustion Generator Config",
                tooltip: () => "List available config options for combustion generator"
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Revamped Combustion Generator",
                tooltip: () => "In Industrial Redux, combustion generator now takes either 3 coals, 3 solar essence or 3 void essence\n to produce 1 battery pack, with the former taking the most time and the latter the least.\n\nDisable to revert back to original combustion generator.",
                getValue: () => Config.NewCombustionGen,
                setValue: value => Config.NewCombustionGen = value
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Seed Makers Config",
                tooltip: () => "List available config options for seed makers."
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Better Quality Seed Makers",
                tooltip: () => "When checked, adding better quality crops to modded seed makers will yield more seeds.\n\nDefault unchecked.",
                getValue: () => Config.BetterSeedMaker,
                setValue: value => Config.BetterSeedMaker = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Static Seed Output",
                tooltip: () => "When checked, seed makers added by this mod will have a constant defined number of seeds produced.\nOtherwise, make them have varied seed amounts.\n\nDefault checked since it worked this way in original mod.",
                getValue: () => Config.StaticSeedMaker,
                setValue: value => Config.StaticSeedMaker = value
            );
            configMenu.AddPage(
                mod: this.ModManifest,
                pageId: "G256.EnabledMachinePage",
                pageTitle: () => "Enable/Disable Machines"
            );
            configMenu.AddParagraph(
                mod: this.ModManifest,
                text: () => "WARNING: Please make sure to disable progress mode if you plan to disable ANY machines."
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Alternator Furnace",
                tooltip: () => "Enable the following:\n   Alternator Furnace",
                getValue: () => Config.EnabledAltEFurnace,
                setValue: value => Config.EnabledAltEFurnace = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable AQUA Fish Catcher",
                tooltip: () => "Enable the following:\n   AQUA Fish Catcher",
                getValue: () => Config.EnabledFishingWell,
                setValue: value => Config.EnabledFishingWell = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable AQUA Gas Smoker",
                tooltip: () => "Enable the following:\n   AQUA Gas Smoker",
                getValue: () => Config.EnabledGasSmoker,
                setValue: value => Config.EnabledGasSmoker = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Auto Miner",
                tooltip: () => "Enable the following:\n   Auto Miner",
                getValue: () => Config.EnabledAutoMiner,
                setValue: value => Config.EnabledAutoMiner = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Ceramic Kiln",
                tooltip: () => "Enable the following:\n   Ceramic Kiln",
                getValue: () => Config.EnabledCeramicKiln,
                setValue: value => Config.EnabledCeramicKiln = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Chromium Furnace",
                tooltip: () => "Enable the following:\n   Chromium Furnace and its heavy variant",
                getValue: () => Config.EnabledChroFurnace,
                setValue: value => Config.EnabledChroFurnace = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Combustion Generator",
                tooltip: () => "Enable the following:\n   Combustion Generator",
                getValue: () => Config.EnabledCombustionGen,
                setValue: value => Config.EnabledCombustionGen = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Combustion Miner",
                tooltip: () => "Enable the following:\n   Combustion Miner",
                getValue: () => Config.EnabledCombustionMiner,
                setValue: value => Config.EnabledCombustionMiner = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Electric Furnace",
                tooltip: () => "Enable the following:\n   Electric Furnace",
                getValue: () => Config.EnabledEFurnace,
                setValue: value => Config.EnabledEFurnace = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Garden Cloche",
                tooltip: () => "Enable the following:\n   Garden Cloche",
                getValue: () => Config.EnabledGardenCloche,
                setValue: value => Config.EnabledGardenCloche = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Gem Polisher",
                tooltip: () => "Enable the following:\n   Gem Polisher",
                getValue: () => Config.EnabledGemPolisher,
                setValue: value => Config.EnabledGemPolisher = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Gold Seed Maker",
                tooltip: () => "Enable the following:\n   Gold Seed Maker",
                getValue: () => Config.EnabledAuSeedMaker,
                setValue: value => Config.EnabledAuSeedMaker = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Hay Bin",
                tooltip: () => "Enable the following:\n   Hay Bin",
                getValue: () => Config.EnabledHayBin,
                setValue: value => Config.EnabledHayBin = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Hay Rehydrator",
                tooltip: () => "Enable the following:\n   Hay Rehydrator",
                getValue: () => Config.EnabledHayRehydrator,
                setValue: value => Config.EnabledHayRehydrator = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Industrial Distillery",
                tooltip: () => "Enable the following:\n   Industrial Distillery",
                getValue: () => Config.EnabledIndustrialDistillery,
                setValue: value => Config.EnabledIndustrialDistillery = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Iridium Seed Maker",
                tooltip: () => "Enable the following:\n   Iridium Seed Maker",
                getValue: () => Config.EnabledIrSeedMaker,
                setValue: value => Config.EnabledIrSeedMaker = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Iron Seed Maker",
                tooltip: () => "Enable the following:\n   Iron Seed Maker",
                getValue: () => Config.EnabledFeSeedMaker,
                setValue: value => Config.EnabledFeSeedMaker = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable JojaCola Generator",
                tooltip: () => "Enable the following:\n   JojaCola Generator",
                getValue: () => Config.EnabledJColaGenerator,
                setValue: value => Config.EnabledJColaGenerator = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Mineral Washer",
                tooltip: () => "Enable the following:\n   Mineral Washer",
                getValue: () => Config.EnabledMineralWasher,
                setValue: value => Config.EnabledMineralWasher = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Preservative Press",
                tooltip: () => "Enable the following:\n   Preservative Press",
                getValue: () => Config.EnabledPreservativePress,
                setValue: value => Config.EnabledPreservativePress = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Pulverizer",
                tooltip: () => "Enable the following:\n   Pulverizer\n   Pulverized Materials",
                getValue: () => Config.EnabledPulverizer,
                setValue: value => Config.EnabledPulverizer = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Rock Crusher",
                tooltip: () => "Enable the following:\n   Rock Crusher",
                getValue: () => Config.EnabledRockCrusher,
                setValue: value => Config.EnabledRockCrusher = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Super Garden Cloche",
                tooltip: () => "Enable the following:\n   Super Garden Cloche",
                getValue: () => Config.EnabledSGardenCloche,
                setValue: value => Config.EnabledSGardenCloche = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enable Tree Cloche",
                tooltip: () => "Enable the following:\n   Tree Cloche",
                getValue: () => Config.EnabledTreeCloche,
                setValue: value => Config.EnabledTreeCloche = value
            );
        }
        private void OnSaveLoaded(object? sender, EventArgs e)
        {
            compensationData = Helper.Data.ReadSaveData<CompensationData>("CompensationGiven") ?? new CompensationData();
            if (!compensationData.Given
                && Game1.player.mailReceived.Contains("G256.IndustrializationRedux.EFurnaceMail")
                && LocalizedContentManager.CurrentLanguageCode == LocalizedContentManager.LanguageCode.zh
                && Config.ProgressMode.Equals(true)
            )
            {
                Game1.addMail("G256.IndustrializationRedux.12bugfixmail");
                Monitor.Log("Compensation mail sent.", LogLevel.Info);
                compensationData.Given = true;
                Helper.Data.WriteSaveData("CompensationGiven", compensationData);
            }

            // Detach the event after it runs once
            Helper.Events.GameLoop.SaveLoaded -= OnSaveLoaded;
        }
        public static bool anyMonoShipped300 = false;
        public static bool anyMonoShipped777 = false;
        public class CompensationData
        {
            public bool Given { get; set; } = false;
        }
        private void OnDayStarted(object? sender, DayStartedEventArgs e)
        {
            foreach (CropData data in Game1.cropData.Values)
            {
                if (data.CountForMonoculture)
                {
                    anyMonoShipped300 = anyMonoShipped300 || FarmerShipped(data.HarvestItemId, 300);
                    anyMonoShipped777 = anyMonoShipped777 || FarmerShipped(data.HarvestItemId, 777);
                }
            }
        }
        public static Item OutputSeedMakerExpanded(Object machine, Item inputItem, bool probe, MachineItemOutput outputData, Farmer player, out int? overrideMinutesUntilReady)
        {
            bool BetterSeedMaker = Config.BetterSeedMaker;
            bool StaticSeedMaker = Config.StaticSeedMaker;
            overrideMinutesUntilReady = null;
            if (!inputItem.HasTypeObject())
            {
                return null;
            }
            string seed = null;
            foreach (KeyValuePair<string, CropData> v in Game1.cropData)
            {
                if (ItemRegistry.HasItemId(inputItem, v.Value.HarvestItemId))
                {
                    seed = v.Key;
                    break;
                }
            }
            if (seed == null)
            {
                return null;
            }
            Vector2 tile = machine.TileLocation;
            Random r = Utility.CreateDaySaveRandom(tile.X, tile.Y * 77f, Game1.timeOfDay);
            if (r.NextDouble() < 0.005)
            {
                return new Object("499", 1);
            }
            if (r.NextDouble() < 0.02)
            {
                return new Object("770", r.Next(1, 5));
            }
            int resultSeeds = 2;
            int minSeeds = 1;
            int maxSeeds = 3;
            if (outputData.CustomData.TryGetValue("SeedsAmount", out string? amountSeeds) && Int32.TryParse(amountSeeds, out resultSeeds)) ;
            if (!StaticSeedMaker)
            {
                minSeeds = resultSeeds - 1;
                maxSeeds = resultSeeds + 1;
            }
            else
            {
                minSeeds = maxSeeds = resultSeeds;
            }
            if (BetterSeedMaker)
            {
                minSeeds += inputItem.Quality;
                maxSeeds += inputItem.Quality;
            }
            return new Object(seed, r.Next(minSeeds, maxSeeds));
        }
        public static Item OutputGardenCloche(Object machine, Item inputItem, bool probe, MachineItemOutput outputData, Farmer player, out int? overrideMinutesUntilReady)
        {
            overrideMinutesUntilReady = null;

            // Check if inputItem is valid
            if (!inputItem.HasTypeObject())
            {
                return null;
            }

            string crop = null;
            int totalStack = 0;
            int totalDays = 1; // Start with 1 day for calculation
            if (MachineDataUtility.TryGetMachineOutputRule(machine, machine.GetMachineData(), MachineOutputTrigger.ItemPlacedInMachine, inputItem, Game1.player, machine.Location, out var _, out MachineOutputTriggerRule triggerRule, out var _, out var _)) { }
            Vector2 tile = machine.TileLocation;
            Random r = Utility.CreateDaySaveRandom(tile.X, tile.Y * 77f, Game1.timeOfDay);

            // Iterate over crop data
            foreach (KeyValuePair<string, CropData> v in Game1.cropData)
            {
                if (ItemRegistry.HasItemId(inputItem, v.Key))
                {
                    crop = v.Value.HarvestItemId;
                    int minStack = v.Value.HarvestMinStack;
                    int maxStack = Math.Max(minStack, v.Value.HarvestMaxStack); // Ensure maxStack is at least minStack

                    // Add stackSum for each iteration of the multiplier
                    for (int i = 0; i < triggerRule.RequiredCount; i++)
                    {
                        int stackSum = r.Next(minStack, maxStack);
                        double extrachance = new Random().NextDouble();
                        if (extrachance < v.Value.ExtraHarvestChance) stackSum++;
                        totalStack += stackSum;
                    }

                    // Calculate total days
                    totalDays += v.Value.DaysInPhase.Sum() - 1; // Subtract 1 for the current day
                    overrideMinutesUntilReady = Utility.CalculateMinutesUntilMorning(Game1.timeOfDay, totalDays);
                    break; // Exit loop once crop is found
                }
            }

            // Return null if crop is not found
            if (crop == null)
            {
                return null;
            }

            // Return Object with calculated values
            return new Object(crop, totalStack, false, -1, 4);
        }


        public static Item OutputGemPolisher(Object machine, Item inputItem, bool probe, MachineItemOutput outputData, Farmer player, out int? overrideMinutesUntilReady)
        {
            overrideMinutesUntilReady = null;
            if (!inputItem.HasTypeObject())
            {
                return null;
            }
            string Input = null;
            foreach (KeyValuePair<string, ObjectData> v in Game1.objectData)
            {
                if (ItemRegistry.HasItemId(inputItem, v.Key))
                {
                    Input = v.Key;
                    overrideMinutesUntilReady = v.Value.Price * 6;
                    break;
                }
            }
            return new Object(Input, 1, false, -1, 4);
        }
        /// <summary>
        /// This is directly taken from vanilla code.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool FarmerShipped(string itemId, int number)
        {
            if (Game1.player.basicShipped.TryGetValue(itemId, out var timesShipped))
            {
                return timesShipped >= number;
            }
            return false;
        }

        public static Item OutputFishingWell(Object machine, Item inputItem, bool probe, MachineItemOutput outputData, Farmer player, out int? overrideMinutesUntilReady)
        {
            Vector2 bobberTile = machine.TileLocation;
            string baitID = null;
            string fishID = null;
            string fishName = null;
            int minStack = 1;
            int maxStack = 1;
            int totalStack = 0;
            int baseOutputStack = 1;
            GameLocation location = machine.Location;
            string locationName = machine.Location.Name;
            int fishCategory = -4;
            int trashCounter = 0;
            
            List<string> nonFishContext = new();
            Random rand = new();
            foreach (KeyValuePair<string, ObjectData> v in Game1.objectData)
            {
                if (ItemRegistry.HasItemId(inputItem, v.Key))
                {
                    baitID = v.Key;
                    break;
                }
            }
            do
            {
                int randomDepth = (int)Math.Floor(Math.Pow(rand.NextDouble(), 2) * 2);
                Item randomFish = location.getFish(0, baitID, randomDepth, player, 1, bobberTile, locationName);
                //Item randomFish = GameLocation.GetFishFromLocationData(locationName, bobberTile, randomDepth, player, false, true, location);
                foreach (KeyValuePair<string, ObjectData> x in Game1.objectData)
                {
                    if (ItemRegistry.HasItemId(randomFish, x.Key))
                    {
                        fishName = x.Value.Name;
                        fishID = x.Key;
                        fishCategory = x.Value.Category;
                        nonFishContext = x.Value.ContextTags;

                        // Check if a fish (not trash) is caught
                        if (fishCategory == -4)
                        {
                            break;  // Exit the loop as soon as a fish is caught
                        }

                        if (fishID.Equals("842")) {
                            continue;
                        }

                        // If it's not a fish, increase the trash counter
                        trashCounter++;

                        // Optional: stop if 10 or more trash items have been caught
                        if (trashCounter >= 10)
                        {
                            break;
                        }
                        
                    }
                }
            } while (trashCounter < 10 && fishCategory != -4 && fishID.Equals("842"));
            Random r = Utility.CreateDaySaveRandom(bobberTile.X, bobberTile.Y * 77f, Game1.timeOfDay);
            if (outputData.CustomData.ContainsKey("G256.MaxBaitCount") &&
                Int32.TryParse(outputData.CustomData["G256.MaxBaitCount"], out int MaxRequiredCount) &&
                MachineDataUtility.TryGetMachineOutputRule(machine, machine.GetMachineData(), MachineOutputTrigger.ItemPlacedInMachine, inputItem, player, machine.Location, out _, out var triggerRule, out _, out _))
            {
                int MinRequiredCount = triggerRule.RequiredCount;
                if (MaxRequiredCount >= MinRequiredCount)
                {
                    baseOutputStack = Math.Min(inputItem.Stack, MaxRequiredCount);
                    Object.ConsumeInventoryItem(player, inputItem, baseOutputStack - MinRequiredCount);
                }
            }
            for (int i = 0; i < baseOutputStack; i++)
            {
                if (baitID.Equals("774"))
                {
                    double chance1 = r.Next(0, 2);
                    double chance2 = ((Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0) ? 1 : 0);
                    if (chance1 == chance2)
                    {
                        maxStack = minStack = 3;
                    };
                }
                if (baitID.Equals("ChallengeBait")) 
                {
                    minStack = 1;
                    maxStack = 4; 
                }
                int stackSum = r.Next(minStack, maxStack); 
                totalStack += stackSum;
            }
            overrideMinutesUntilReady = null;
            return new Object(fishID, totalStack);
        }
    }
}