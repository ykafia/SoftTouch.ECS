using System.Diagnostics;

namespace SoftTouch.ECS;

public class AppTime
{
    public TimeSpan Elapsed { get; private set; }
    public TimeSpan WarpElapsed { get; private set; }
    public TimeSpan TotalElapsed { get; private set; }
    public double FramePerSecond => Elapsed.TotalSeconds > 0 ?  1d / Elapsed.TotalSeconds : 0;
    public long FrameCount { get; private set; }
    public double Factor { get; set; }
    readonly Stopwatch stopwatch;

    public AppTime() 
    { 
        stopwatch = new();
        Factor = 1;
    }

    public void Update()
    {
        if (stopwatch.IsRunning)
        {
            FrameCount += 1;
            Elapsed = stopwatch.Elapsed;
            WarpElapsed = Elapsed.Multiply(Factor);
            TotalElapsed.Add(Elapsed);
            stopwatch.Restart();
        }
        else 
            stopwatch.Start();
    }
}