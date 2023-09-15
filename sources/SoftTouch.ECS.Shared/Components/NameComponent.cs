
namespace SoftTouch.ECS.Shared.Components
{
    public record struct NameComponent
    {
        public string Name { get; set; }
        public NameComponent(string name) { this.Name = name; }

        public static implicit operator NameComponent(string name) => new(name);
    }
}