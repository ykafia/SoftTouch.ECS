namespace SoftTouch.ECS.Scheduling;


#region Startup

/// <summary>
/// Base record for Startup stages
/// </summary>
public abstract class StartupBase : SubStage<Main>;

/// <summary>
/// Runs once before the startup stage.
/// </summary>
public sealed class PreStartup : StartupBase;
/// <summary>
/// Runs once at the start of the app.
/// </summary>
public sealed class Startup : StartupBase;
/// <summary>
/// Runs once after the startup stage.
/// </summary>
public sealed class PostStartup : StartupBase;
#endregion

#region Main
/// <summary>
/// Runs first before everything else.
/// </summary>
public sealed class First : SubStage<Main>;
/// <summary>
/// Runs before the Update stage.
/// </summary>
public sealed class PreUpdate : SubStage<Main>;
/// <summary>
/// Runs everytime, contains the main logic.
/// </summary>
public sealed class Update : SubStage<Main>;
/// <summary>
/// Runs after the Update stage.
/// </summary>
public sealed class PostUpdate : SubStage<Main>;
/// <summary>
/// Runs last after everything else.
/// </summary>
public sealed class Last : SubStage<Main>;
#endregion