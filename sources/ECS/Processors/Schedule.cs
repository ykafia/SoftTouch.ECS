namespace ECSharp;

public struct Schedule
{
    public string Label { get; set; } = "Default";

    public Schedule(string label)
    {
        Label = label;
    }

    public static implicit operator string(Schedule s) => s.Label;
    public static explicit operator Schedule(string s) => new Schedule(s);
}