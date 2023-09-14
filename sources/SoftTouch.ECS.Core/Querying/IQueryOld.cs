using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;

public interface IWithWorld
{
    void WithWorld(World world);
}

public interface IQuery : IWithWorld
{
    
}
