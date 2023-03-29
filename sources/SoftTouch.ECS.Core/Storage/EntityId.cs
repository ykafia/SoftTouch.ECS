using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Storage;

public readonly struct EntityId
{
    readonly int id;

    public EntityId(int id)
    {
        this.id = id;
    }

    public static implicit operator int(EntityId eid) => eid.id;
}
