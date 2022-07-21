using System;
using System.Collections.Generic;
using System.Linq;
using ECSharp.ComponentData;

namespace ECSharp.Arrays
{

    public class ComponentArrayStruct<T> : ComponentArrayBase 
        where T : struct
    {

        public static ComponentArrayStruct<T> Empty = new ComponentArrayStruct<T>();

        public List<T> Elements = new();
        public override int GetLength() => Elements.Count;
        public Type ElementType => typeof(T);

        #region constructor
        public ComponentArrayStruct()
        {
            this.Elements = new List<T>();
        }

        public ComponentArrayStruct(T element)
        {
            this.Elements = new List<T>{element};
        }
        public ComponentArrayStruct(List<T> elements)
        {
            this.Elements = elements;
        }
        #endregion

        public T this[int i]
        {
            get{return Elements[i];}
            set{Elements[i] = value;}
        }

        #region operations
        public void Add(T e) => Elements.Add(e);
        
        public void Remove(T e) => Elements.Remove(e);

        public override ComponentBase RemoveAt(int i)
        {
            ComponentStruct<T> cmp = new(Elements[i]);
            Elements.RemoveAt(i);
            return cmp;
        }

        

        #endregion

        

        public override Type GetElementType()
        {
            return ElementType;
        }
        public override string StringRepresentation()
        {
            return string.Join("; ",Elements.Select(x => x.ToString()));
        }

        public void Add(ComponentStruct<T> c)
        {
            Elements.Add(c.Value);
        }

        

        public override void AddComponents(List<ComponentBase> components)
        {
            throw new NotImplementedException();
        }

        public override void Add(ComponentBase c)
        {
            if(c is ComponentStruct<T> r)
                Elements.Add(r.Value);
            else
                throw new Exception("Attempted to add a wrong component");
        }

        public override ComponentArrayBase New(ComponentBase c)
        {
            throw new NotImplementedException();
        }

        public void TransferTo(ComponentArrayStruct<T> other, int index)
        {
            var transferred = Elements[index];
            Elements.RemoveAt(index);
            other.Add(transferred);
        }

        public override void TransferTo(ComponentArrayBase other, int index)
        {
            if(other is ComponentArrayStruct<T> othera)
                TransferTo(othera, index);
            else
                throw new Exception("Wrong array type");
        }

        public override ComponentArrayBase New(Type t)
        {
            if(typeof(T) == t)
                return new ComponentArrayStruct<T>();
            else
                throw new Exception("Wrong array type");
        }
    }
}