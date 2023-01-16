
namespace SoftTouch.ECS.Shared.Components
{
    public struct NameComponent
    {
        public string Name { get; set; }
        public NameComponent(string name) { this.Name = name; }
    }
}