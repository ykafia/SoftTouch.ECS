using System.Text;

namespace SoftTouch.ECS.Scheduling;

public class ProcessorGroups : List<Group>
{
    public override string ToString()
    {
        var b = new StringBuilder().Append('[');
        if(Count == 0)
            return b.Append(']').ToString();
        bool start = true;

        foreach(var e in this)
            if(start)
            {
                start = false;
                b.Append(e);
            }
            else
                b.Append(", ").Append(e);
        return b.Append(']').ToString();
    }
}
