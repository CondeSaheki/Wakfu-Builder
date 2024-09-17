namespace WakfuBuider;

public class Sublimation
{
    public int Id { get; set; } = 0;
    public int? Level { get; set; } = null;
    public int? Parent { get; set; } = null;

    public SublimationType Type { get; set; } = new();
    public EnchatmentType[] Combination { get; set; } = [];
    public LocalizedString Name { get; set; } = new();
    public LocalizedString Description { get; set; } = new();
    public int Coincidents { get; set; } = 0;
    public SublimationState? State { get; set; } = null;
    public string ToString(Localization locale = Localization.English) => $"> {Name.Local(locale)} | {Type} | {string.Join(", ", Combination)} | {Coincidents}\n{Description.Local(locale)}";
}

public class SublimationState
{
    public int Id { get; set; } = 0;
    public LocalizedString Name { get; set; } = new();
}

