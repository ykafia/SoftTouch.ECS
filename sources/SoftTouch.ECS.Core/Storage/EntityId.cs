using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Storage;

public readonly record struct EntityId(int Id)
{
    public static implicit operator int(EntityId eid) => eid.Id;
}
