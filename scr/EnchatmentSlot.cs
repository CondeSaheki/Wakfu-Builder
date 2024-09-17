namespace WakfuBuider;

public class EnchatmentSlot
{
    public EnchatmentType? Type { get; set; } = null;
    public Enchantment? Effect { get; set; } = null;
    public uint? Level { get; set; } = null;

    public static bool IsValid(Item item, EnchatmentSlot enchatment)
    {
        if (enchatment.Effect == null && enchatment.Level == null) return true;
        if (enchatment.Type == null && (enchatment.Effect != null || enchatment.Level != null)) return false;

        if (enchatment.Type == EnchatmentType.Red && !Enchantment.Red.Contains(enchatment.Effect)) return false;
        if (enchatment.Type == EnchatmentType.Green && !Enchantment.Green.Contains(enchatment.Effect)) return false;
        if (enchatment.Type == EnchatmentType.Blue && !Enchantment.Blue.Contains(enchatment.Effect)) return false;
        if (enchatment.Type == EnchatmentType.White && !Enchantment.White.Contains(enchatment.Effect)) return false;

        if (1 > enchatment.Level) return false;

        try
        {
            var max = Enchantment.MaxLevel(item);
            if (max >= enchatment.Level) return true;
            return false;
        }
        catch
        {
            return false;
        }
    }
}