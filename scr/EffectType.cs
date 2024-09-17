namespace WakfuBuider;

public enum EffectType
{
    HP = 20,
    NegativeHP = 21,
    AP = 31,
    NegativeMaxAp = 56,
    MP = 41,
    NegativeMaxMP = 57,
    NegativeMP = 42, // ?
    WP = 191,
    NegativeMaxWP = 192,

    ElementalMastery = 120,
    NegativeElementalMastery = 130,
    ElementalResistance = 80,
    NegativeElementalResistance = 100,
    NegativeElementalResistance2 = 90, // why 2 ids for the same thing ?
    FireMastery = 122,
    NegativeFireMastery = 132,
    WaterMastery = 124,
    EarthMastery = 123,
    AirMastery = 125,
    FireResistance = 82,
    NegativeFireResistance = 97,
    WaterResistance = 83,
    NegativeWaterResistance = 98,
    EarthResistance = 84,
    NegativeEarthResistance = 96,
    AirResistance = 85,
    MasteryofXrandomelement = 1068,
    ResistancetoXRandomElements = 1069,

    CriticalHit = 150,
    NegativeCriticalHit = 168,
    Initiative = 171,
    NegativeInitiative = 172,
    Dodge = 175,
    NegativeDodge = 176,
    Wisdom = 166,
    Control = 184,
    Block = 875,
    NegativeBlock = 876,
    Range = 160,
    NegativeRange = 161,
    Lock = 173,
    NegativeLock = 174,
    Prospecting = 162,
    ForceofWill = 177,

    CriticalMastery = 149,
    NegativeCriticalMastery = 1056,
    RearMastery = 180,
    NegativeRearMastery = 181,
    MeleeMastery = 1052,
    NegativeMeleeMastery = 1059,
    DistanceMastery = 1053,
    NegativeDistanceMastery = 1060,
    HealingMastery = 26,
    BerserkMastery = 1055,
    NegativeBerserkMastery = 1061,
    CriticalResistance = 988,
    NegativeCriticalResistance = 1062,
    RearResistance = 71,
    NegativeRearResistance = 1063,
    Armorgiven = 39,
    NegativeArmorreceived = 40,

    // weird
    ReflectsXofdamage = 1020,
    XLvltoXspells = 832,
    XLvltoelementalspells = 979,

    // ignore
    HarvestingQuantity = 2001,
    Unknown = 304, // sublimations ?
    MovementSpeed = 400, // need double check
}
