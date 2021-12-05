using System;
using System.Collections.Generic;

namespace ECSharp
{
    public class ArchetypeEdges
    {
        public Dictionary<Type,Archetype> Add = new();
        public Dictionary<Type,Archetype> Remove = new();
    }
}