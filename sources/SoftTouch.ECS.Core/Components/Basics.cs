namespace SoftTouch.ECS.Components;


public struct Name(in string name)
{
    public string Value { get; set; } = name;
    
    public static implicit operator Name(string name) => new(name);
    public static implicit operator string(Name name) => name.Value;
}