using SoftTouch.ECS.Attributes;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

// TODO: Create a source generator to generate bundles with marked functions
public interface IProcessorBundle
{
    public App AddBundleElements(App app);
}