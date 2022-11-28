using System.Diagnostics;

namespace ECSharp;

public class WorldTimer
{
    public TimeSpan Elapsed {get; private set;}
    public TimeSpan WarpElapsed {get; private set;}
    public TimeSpan TotalElapsed {get; private set;}
    public long FrameCount {get; private set;}
    public double Factor {get;set;} = 1;

    Stopwatch stopwatch = new();
    public WorldTimer(){}

    public void Start() => stopwatch.Start();
    public void StopAndUpdate()
    {
        stopwatch.Stop();
        FrameCount+=1;
        Update(stopwatch.Elapsed);
        stopwatch.Reset();
    }

    public void Update(TimeSpan elapsed)
    {
        Elapsed = elapsed;
        WarpElapsed = elapsed.Multiply(Factor);
        TotalElapsed.Add(Elapsed);
    }
}