using SoftTouch.ECS.Arrays;
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

    /// <summary>
    /// Set sub stages of specified stage and dispose of the ReusableList used as parameter.
    /// </summary>
    /// <typeparam name="TStage"></typeparam>
    /// <param name="subStages">ReadOnlyList of sub stages to be set in the Stage specified. The list is disposed after calling the function</param>
    internal void SetStages<TStage>(ReusableList<SubStage<TStage>> subStages)
        where TStage : Stage
    {
        if(main is TStage && subStages is ReusableList<SubStage<Main>> rm)
            main.SetStages<Main>(rm.Span);
        else if(extract is TStage && subStages is ReusableList<SubStage<Extract>> re)
            extract.SetStages<Extract>(re.Span);
        else if(render is TStage && subStages is ReusableList<SubStage<Render>> rr)
            render.SetStages<Render>(rr.Span);
        subStages.Dispose();
    }

    public override string ToString()
    {
        return $"Main : {main}\nRender : {render}\nExtract : {extract}";
    }

}