using SoftTouch.ECS.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scheduling;

public class StageNode : Node
{
    public string Name { get; set; }
    public List<Processors.Processor> Processors { get; set; }
}
