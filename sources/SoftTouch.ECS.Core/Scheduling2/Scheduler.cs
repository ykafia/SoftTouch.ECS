using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling2;

public class Scheduler(App app)
{
    public App App { get; } = app;
    readonly Main main = new();
    readonly Extract extract = new();
    readonly Render render = new();

    public void Update()
    {
        main.Update();
        if(App is SubApp)
            render.Update();
    }

    public void RunExtract()
    {
        extract.Update();
    }

    #error Finish this
    public void Add<TStage, TProcessor>()
        where TStage : SubStage
        where TProcessor : Processor
    {
        if (typeof(TStage).IsSubclassOf(typeof(SubStage<Main>)))
        {
            
        }
        else if(typeof(TStage).IsSubclassOf(typeof(SubStage<Extract>)))
        {

        }
        else if(typeof(TStage).IsSubclassOf(typeof(SubStage<Render>)))
        {
            
        }
        else
        {
            throw new ArgumentException("TStage must be a derived type of SubStage<Main>");
        }
    }

}