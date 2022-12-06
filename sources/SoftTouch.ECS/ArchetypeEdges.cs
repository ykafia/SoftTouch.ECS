using System;
using System.Collections.Generic;

namespace SoftTouch.ECS
{
    public class ArchetypeEdges
    {
        public Dictionary<Type,Archetype> Add = new();
        public Dictionary<Type,Archetype> Remove = new();
    }
}