namespace WakfuBuider;

public class Build
{
    public ItemIndentified? Amulet { get; set; } = null;
    public ItemIndentified? RingOne { get; set; } = null;
    public ItemIndentified? RingTwo { get; set; } = null;
    public ItemIndentified? Boot { get; set; } = null;
    public ItemIndentified? Cloak { get; set; } = null;
    public ItemIndentified? Belt { get; set; } = null;
    public ItemIndentified? Helmet { get; set; } = null;
    public ItemIndentified? Epaulette { get; set; } = null;
    public ItemIndentified? Breastplate { get; set; } = null;
    public ItemIndentified? Emblem { get; set; } = null;
    public ItemIndentified? Pet { get; set; } = null;
    public ItemIndentified? MainHand { get; set; } = null;
    public ItemIndentified? OffHand { get; set; } = null;  // This can be null if using a two-handed weapon

    public static bool IsComplete(Build build)
    {
        if (build.Amulet == null ||
            build.RingOne == null ||
            build.RingTwo == null ||
            build.Boot == null ||
            build.Cloak == null ||
            build.Belt == null ||
            build.Helmet == null ||
            build.Epaulette == null ||
            build.Breastplate == null ||
            build.Emblem == null ||
            build.Pet == null) return false;

        if (!IsWeaponConfigurationValid(build)) return false;
        return true;
    }

    public static bool IsValid(Build build)
    {
        if (build.Amulet != null && !ItemIndentified.IsValid(build.Amulet) ||
            build.RingOne != null && !ItemIndentified.IsValid(build.RingOne) ||
            build.RingTwo != null && !ItemIndentified.IsValid(build.RingTwo) ||
            build.Boot != null && !ItemIndentified.IsValid(build.Boot) ||
            build.Cloak != null && !ItemIndentified.IsValid(build.Cloak) ||
            build.Belt != null && !ItemIndentified.IsValid(build.Belt) ||
            build.Helmet != null && !ItemIndentified.IsValid(build.Helmet) ||
            build.Epaulette != null && !ItemIndentified.IsValid(build.Epaulette) ||
            build.Breastplate != null && !ItemIndentified.IsValid(build.Breastplate) ||
            build.Emblem != null && !ItemIndentified.IsValid(build.Emblem) ||
            build.Pet != null && !ItemIndentified.IsValid(build.Pet) ||
            build.MainHand != null && !ItemIndentified.IsValid(build.MainHand) ||
            build.OffHand != null && !ItemIndentified.IsValid(build.OffHand)) return false;

        if ((build.Amulet != null && build.Amulet.Type != ItemType.Amulet) ||
            (build.RingOne != null && build.RingOne.Type != ItemType.Ring) ||
            (build.RingTwo != null && build.RingTwo.Type != ItemType.Ring) ||
            (build.Boot != null && build.Boot.Type != ItemType.Boot) ||
            (build.Cloak != null && build.Cloak.Type != ItemType.Cloak) ||
            (build.Belt != null && build.Belt.Type != ItemType.Belt) ||
            (build.Helmet != null && build.Helmet.Type != ItemType.Helmet) ||
            (build.Epaulette != null && build.Epaulette.Type != ItemType.Epaulette) ||
            (build.Breastplate != null && build.Breastplate.Type != ItemType.Breastplate) ||
            (build.Emblem != null && build.Emblem.Type != ItemType.Emblem) ||
            (build.Pet != null && build.Pet.Type != ItemType.Pet)) return false;

        // rings must be different
        if (build.RingOne != null && build.RingTwo != null && build.RingOne.ID == build.RingTwo.ID) return false;

        if (!IsWeaponConfigurationValid(build)) return false;

        int epiqueCount = 0;
        if (build.Amulet != null && build.Amulet.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.RingOne != null && build.RingOne.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.RingTwo != null && build.RingTwo.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Boot != null && build.Boot.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Cloak != null && build.Cloak.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Belt != null && build.Belt.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Helmet != null && build.Helmet.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Epaulette != null && build.Epaulette.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Breastplate != null && build.Breastplate.Rarity == Rarity.Epique) ++epiqueCount;
        if (epiqueCount > 1) return false;

        int reliceCount = 0;
        if (build.Amulet != null && build.Amulet.Rarity == Rarity.Relic) ++reliceCount;
        if (build.RingOne != null && build.RingOne.Rarity == Rarity.Relic) ++reliceCount;
        if (build.RingTwo != null && build.RingTwo.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Boot != null && build.Boot.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Cloak != null && build.Cloak.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Belt != null && build.Belt.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Helmet != null && build.Helmet.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Epaulette != null && build.Epaulette.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Breastplate != null && build.Breastplate.Rarity == Rarity.Relic) ++reliceCount;
        if (reliceCount > 1) return false;

        return true;
    }

    private static bool IsWeaponConfigurationValid(Build build)
    {
        if (build.MainHand != null && Item.IsTwoHandWeapon(build.MainHand)) return build.OffHand == null;
        if (build.MainHand != null && Item.IsOneHandWeapon(build.MainHand)) return build.OffHand == null || Item.IsOffHandWeapon(build.OffHand);
        return false;
    }
}
