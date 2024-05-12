namespace SoftTouch.ECS.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class BundleAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}