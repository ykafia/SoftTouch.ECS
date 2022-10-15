using System.Diagnostics;

namespace ECSharp;

public class WorldTimer
{
    public TimeSpan Elapsed {get; private set;}
    public TimeSpan WarpElapsed {get; private set;}
    public TimeSpan TotalElapsed {get; private set;}
    public double Factor {get;set;} = 1;

    public WorldTimer(){}

    public void Update(TimeSpan elapsed)
    {
        Elapsed = elapsed;
        WarpElapsed = elapsed.Multiply(Factor);
        TotalElapsed.Add(Elapsed);
    }
}