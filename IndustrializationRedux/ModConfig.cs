using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrializationRedux
{
    public sealed class ModConfig
    {
        public bool MachinesSale { get; set; } = true;
        public bool UseOres { get; set; } = true;
        public bool BetterSeedMaker { get; set; } = false;
        public bool ProgressMode { get; set; } = true;
        public bool StaticSeedMaker { get; set; } = true;

        public bool EnabledAltEFurnace { get; set; } = true;
        public bool EnabledEFurnace { get; set; } = true;
        public bool EnabledAutoMiner { get; set; } = true;
        public bool EnabledCeramicKiln { get; set; } = true;
        public bool EnabledChroFurnace { get; set; } = true;
        public bool EnabledCombustionGen { get; set; } = true;
        public bool EnabledCombustionMiner { get; set; } = true;
        public bool EnabledGardenCloche { get; set; } = true;
        public bool EnabledGemPolisher { get; set; } = true;
        public bool EnabledHayBin { get; set; } = true;
        public bool EnabledHayRehydrator { get; set; } = true;
        public bool EnabledIndustrialDistillery { get; set; } = true;
        public bool EnabledJColaGenerator { get; set; } = true;
        public bool EnabledMineralWasher { get; set; } = true;
        public bool EnabledPreservativePress { get; set; } = true;
        public bool EnabledPulverizer { get; set; } = true;
        public bool EnabledRockCrusher { get; set; } = true;
        public bool EnabledSGardenCloche { get; set; } = true;
        public bool EnabledTreeCloche { get; set; } = true;
        public bool EnabledFeSeedMaker { get; set; } = true;
        public bool EnabledAuSeedMaker { get; set; } = true;
        public bool EnabledIrSeedMaker { get; set; } = true;
        public bool EnabledGasSmoker { get; set; } = true;
        public bool EnabledFishingWell { get; set; } = true;
        public bool NewCombustionGen {  get; set; } = true;

    }
}
