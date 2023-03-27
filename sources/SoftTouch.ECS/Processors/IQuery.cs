using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;

public interface IWithWorld<W>
    where W : World
{
    void WithWorld(W world);
}

public interface IQuery<W> : IWithWorld<W>
    where W : World
{
    
}
