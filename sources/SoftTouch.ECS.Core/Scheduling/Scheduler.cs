using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public class Scheduler(App app)
{
    public App App { get; } = app;
    readonly Main main = new();
    readonly Extract extract = new();
    readonly Render render = new();

    public void Run(bool parallel = true)
    {
        main.Update(parallel);
        if(App is RenderApp)
            render.Update(false);
    }

    public void RunExtract()
    {
        extract.Update();
    }

    public void Add<TStage>(ReadOnlySpan<Processor> processors)
        where TStage : SubStage
    {
        foreach(var processor in processors)
            Add<TStage>(processor);
    }

    public void Add<TStage>(Processor processor)
        where TStage : SubStage
    {
        if (typeof(TStage).IsSubclassOf(typeof(SubStage<Main>)))
            main.TryAdd<TStage>(processor);
        else if(typeof(TStage).IsSubclassOf(typeof(SubStage<Extract>)))
            extract.TryAdd<TStage>(processor);
        else if(typeof(TStage).IsSubclassOf(typeof(SubStage<Render>)))
            render.TryAdd<TStage>(processor);
        else
            throw new ArgumentException("TStage must be a derived type of SubStage<Main>");
    }

}