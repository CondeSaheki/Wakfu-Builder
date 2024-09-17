namespace WakfuBuider;

public class Test
{
    public string Name { get; set; } = string.Empty;
    public float? Result { get; private set; } = null;
    public Func<Build, float> Work { get; set; } = (x) => 0;

    public void Calculate(Build build)
    {
        if (Result != null) Result = Work.Invoke(build);
    }
}