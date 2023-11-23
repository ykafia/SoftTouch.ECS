namespace SoftTouch.ECS;


public delegate void ExtractMethod(World parentWorld, App subApp);

public class SubApp(App app, ExtractMethod extract)
{
    public App App { get; init; } = app;
    public ExtractMethod ExtractDelegate { get; set; } = extract;
    public void Extract(World parentWorld) => ExtractDelegate.Invoke(parentWorld, App);
}
