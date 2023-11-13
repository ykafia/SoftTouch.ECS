namespace SoftTouch.ECS;


public delegate void ExtractMethod(World parentWorld, App subApp);

public class SubApp
{
    public App App { get; init; }
    public ExtractMethod ExtractDelegate { get; set; }

    public SubApp(App app)
    {
        App = app;
        ExtractDelegate = static (_,_) => {}; 
    }
}
