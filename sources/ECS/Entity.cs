using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECSharp
{
    public interface IEntity{}

    public class Entity
    {
        public long Index{get;set;}
        public World? World{get;set;}
    }
    
}