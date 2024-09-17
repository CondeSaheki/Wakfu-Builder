namespace WakfuBuider;

public class Build
{
    public ItemIndentified? Amulet { get; set; } = null;
    public ItemIndentified? RingOne { get; set; } = null;
    public ItemIndentified? RingTwo { get; set; } = null;
    public ItemIndentified? Boots { get; set; } = null;
    public ItemIndentified? Cape { get; set; } = null;
    public ItemIndentified? Belt { get; set; } = null;
    public ItemIndentified? Helmet { get; set; } = null;
    public ItemIndentified? Epaulettes { get; set; } = null;
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
            build.Boots == null ||
            build.Cape == null ||
            build.Belt == null ||
            build.Helmet == null ||
            build.Epaulettes == null ||
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
            build.Boots != null && !ItemIndentified.IsValid(build.Boots) ||
            build.Cape != null && !ItemIndentified.IsValid(build.Cape) ||
            build.Belt != null && !ItemIndentified.IsValid(build.Belt) ||
            build.Helmet != null && !ItemIndentified.IsValid(build.Helmet) ||
            build.Epaulettes != null && !ItemIndentified.IsValid(build.Epaulettes) ||
            build.Breastplate != null && !ItemIndentified.IsValid(build.Breastplate) ||
            build.Emblem != null && !ItemIndentified.IsValid(build.Emblem) ||
            build.Pet != null && !ItemIndentified.IsValid(build.Pet) ||
            build.MainHand != null && !ItemIndentified.IsValid(build.MainHand) ||
            build.OffHand != null && !ItemIndentified.IsValid(build.OffHand)) return false;

        if ((build.Amulet != null && build.Amulet.Type != ItemType.Amulet) ||
            (build.RingOne != null && build.RingOne.Type != ItemType.Ring) ||
            (build.RingTwo != null && build.RingTwo.Type != ItemType.Ring) ||
            (build.Boots != null && build.Boots.Type != ItemType.Boots) ||
            (build.Cape != null && build.Cape.Type != ItemType.Cloak) ||
            (build.Belt != null && build.Belt.Type != ItemType.Belt) ||
            (build.Helmet != null && build.Helmet.Type != ItemType.Helmet) ||
            (build.Epaulettes != null && build.Epaulettes.Type != ItemType.Epaulettes) ||
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
        if (build.Boots != null && build.Boots.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Cape != null && build.Cape.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Belt != null && build.Belt.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Helmet != null && build.Helmet.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Epaulettes != null && build.Epaulettes.Rarity == Rarity.Epique) ++epiqueCount;
        if (build.Breastplate != null && build.Breastplate.Rarity == Rarity.Epique) ++epiqueCount;
        if (epiqueCount > 1) return false;

        int reliceCount = 0;
        if (build.Amulet != null && build.Amulet.Rarity == Rarity.Relic) ++reliceCount;
        if (build.RingOne != null && build.RingOne.Rarity == Rarity.Relic) ++reliceCount;
        if (build.RingTwo != null && build.RingTwo.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Boots != null && build.Boots.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Cape != null && build.Cape.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Belt != null && build.Belt.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Helmet != null && build.Helmet.Rarity == Rarity.Relic) ++reliceCount;
        if (build.Epaulettes != null && build.Epaulettes.Rarity == Rarity.Relic) ++reliceCount;
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
