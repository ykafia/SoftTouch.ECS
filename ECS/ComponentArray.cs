using System;
using System.Collections.Generic;
using System.Linq;

namespace WonkECS
{

    public partial class ComponentArray
    {
        public virtual string StringRepresentation() => "";
        public virtual void AddComponents(List<object> components){}
        public virtual void Add(ComponentBox c){}
        public virtual ComponentBox RemoveAt(int i) => null;
        
        public virtual ComponentArray New(ComponentBox c) => null;
        
        public virtual Type GetElementType() => GetType();
        public virtual void Merge(ComponentArray other){}
        public virtual ComponentArray EmptyFrom(ComponentArray array) => new();
        public virtual int GetLength() => 0;
        public virtual List<object> GetArray() => new();
    }

    public class ComponentArray<T> : ComponentArray where T : struct
    {
        public List<T> Elements = new();
        public override int GetLength() => Elements.Count;

        public Type ElementType => typeof(T);

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

        public override ComponentArray New(ComponentBox c) => new ComponentArray<T>((T)c.Get());

        public void Add(T e) => Elements.Add(e);

        public void Remove(T e) => Elements.Remove(e);

        public override ComponentBox RemoveAt(int i)
        {
            ComponentBox<T> cmp = new(Elements[i]);
            Elements.RemoveAt(i);
            return cmp;
        }

        public T this[int i]
        {
            get{return Elements[i];}
            set{Elements[i] = value;}
        }

        public override Type GetElementType()
        {
            return ElementType;
        }

        public override void Add(ComponentBox c)
        {
            Add((T)c.Get());
        }

        public override string StringRepresentation()
        {
            return string.Join("; ",Elements.Select(x => x.ToString()).ToList());
        }
        public override void Merge(ComponentArray other)
        {
            if(other.GetElementType() == ElementType) Elements.AddRange(other.GetArray().Cast<T>());
        }
        public override List<object> GetArray()
        {
            return Elements.Cast<object>().ToList();
        }

        public override ComponentArray EmptyFrom(ComponentArray array) => new ComponentArray<T>(new List<T>());

    }
}