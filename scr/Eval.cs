namespace WakfuBuider;

public class Eval
{
    public Build Build { get; set; } = new();
    public Entity Entity { get; set; } = new();
    public Test[] Tests { get; set; } = [];

    public void Calculate()
    {
        foreach (var test in Tests) test.Calculate(Build);
    }
}