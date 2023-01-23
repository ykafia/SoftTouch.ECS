using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoftTouch.ECS.Storage;

public readonly struct EntityId
{
    readonly long id;

    public EntityId(long id)
    {
        this.id = id;
    }

    public static implicit operator long(EntityId eid) => eid.id;
    public static implicit operator EntityId(long id) => new(id);
}
