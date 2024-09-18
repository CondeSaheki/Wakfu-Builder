namespace WakfuBuider;

public class ItemIndentified : Item 
{
    public Dictionary<int, Element[]>? RandomElement { get; set; } = null;

    public EnchatmentSlot Slot1 { get; set; } = new();
    public EnchatmentSlot Slot2 { get; set; } = new();
    public EnchatmentSlot Slot3 { get; set; } = new();
    public EnchatmentSlot Slot4 { get; set; } = new();

    public Sublimation? Sublimation { get; set; } = null;
    public Sublimation? EspecialSublimation { get; set; } = null;

    public static bool IsValid(ItemIndentified item)
    {
        if (Item.IsValid(item)) return false;
        if (!EnchatmentSlot.IsValid(item, item.Slot1)) return false;
        if (!EnchatmentSlot.IsValid(item, item.Slot2)) return false;
        if (!EnchatmentSlot.IsValid(item, item.Slot3)) return false;
        if (!EnchatmentSlot.IsValid(item, item.Slot4)) return false;

        if (item.Slot2.Type != null && item.Slot1.Type == null) return false;
        if (item.Slot3.Type != null && (item.Slot1.Type == null || item.Slot2.Type == null)) return false;
        if (item.Slot4.Type != null && (item.Slot1.Type == null || item.Slot2.Type == null || item.Slot3.Type == null)) return false;

        if (item.Sublimation != null)
        {
            if (Item.IsOffHandWeapon(item)) return false;
            if (item.Sublimation.Type != SublimationType.Normal) return false;
            if (item.Sublimation.Combination.Length != 0)
            {
                List<EnchatmentType> slots = [];
                if (item.Slot1.Type != null) slots.Add((EnchatmentType)item.Slot1.Type);
                if (item.Slot2.Type != null) slots.Add((EnchatmentType)item.Slot2.Type);
                if (item.Slot3.Type != null) slots.Add((EnchatmentType)item.Slot3.Type);
                if (item.Slot4.Type != null) slots.Add((EnchatmentType)item.Slot4.Type);
                if (item.Sublimation.Combination.Length > slots.Count) return false;
                if (!Util.ContainsSequence([.. slots], item.Sublimation.Combination)) return false;
            }
        }

        if (item.EspecialSublimation != null)
        {
            if (item.Rarity != Rarity.Relic && item.Rarity != Rarity.Epique) return false;
            if (item.EspecialSublimation.Type == SublimationType.Normal) return false;
        }
        return true;
    }
}