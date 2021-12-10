using System;
using System.Collections.Generic;
using System.Linq;
using ECSharp.ComponentData;

namespace ECSharp.Arrays
{

    public class ComponentArray<T> : ComponentArrayBase where T : Component
    {
        public List<T> Elements = new();
        public override int GetLength() => Elements.Count;
        public Type ElementType => typeof(T);

        #region constructor
        public ComponentArray()
        {
            this.Elements = new List<T>();
        }

        public ComponentArray(T element)
        {
            this.Elements = new List<T>{element};
        }
        public ComponentArray(List<T> elements)
        {
            this.Elements = elements;
        }
        #endregion

        public T this[int i]
        {
            get{return Elements[i];}
            set{Elements[i] = value;}
        }
        
        public override ComponentArrayBase New(object c) => new ComponentArray<T>();
        #region operations
        public void Add(T e) => Elements.Add(e);
        public void Remove(T e) => Elements.Remove(e);

        public override T RemoveAt(int i)
        {
            T cmp = Elements[i];
            Elements.RemoveAt(i);
            return cmp;
        }

        public override void Merge(ComponentArrayBase other)
        {
            if(other.GetElementType() == ElementType) Elements.AddRange(other.GetArray().Cast<T>());
        }
        public override List<object> GetArray()
        {
            return Elements.Cast<object>().ToList();
        }

        #endregion

        

        public override Type GetElementType()
        {
            return ElementType;
        }

        public override ComponentArrayBase EmptyFrom(ComponentArrayBase array) => new ComponentArray<T>();
        

        public override string StringRepresentation()
        {
            return string.Join("; ",Elements.Select(x => x.ToString()).ToList());
        }
        
    }
}