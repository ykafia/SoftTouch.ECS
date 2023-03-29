using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scripts;

public abstract class EntityScript
{
    World w;
    List<int> entities { get; set; }
    public EntityScript(World w)
    {
        entities = new();
    }

    public EntityScript(World w, params int[] entityIds)
    {
        
    }
}
