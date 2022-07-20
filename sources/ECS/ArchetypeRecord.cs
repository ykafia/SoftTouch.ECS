using System.Collections.Generic;
using System.Linq;
using ECSharp.ComponentData;

namespace ECSharp
{
    public class ArchetypeRecord
    {
        public Entity Entity;
        public Archetype Archetype;

        public int ArchetypeIndex => Archetype.EntityID.IndexOf(Entity.Index);
        public int Index => (int)Entity.Index;

        public override string ToString()
        {
            return Archetype.ToString();
        }

        public void Set<T>(T cmp) where T : struct
            => Archetype.SetComponent(ArchetypeIndex, cmp);
        
        public T Get<T>() where T : struct
            => Archetype.GetComponentArray<T>()[ArchetypeIndex];
        public bool Has<T>()
            => Archetype.Storage.ContainsKey(typeof(T));


        public void Add<T>(in T c) where T : struct
        {
            List<ComponentBase> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(ArchetypeIndex)).ToList();
            comps.Add(new ComponentStruct<T>(c));
            if(Archetype.Edges.Add.TryGetValue(typeof(T), out var newArch))
            {
                // Add all components to new archetype
                foreach(var cmp in comps)
                    newArch.Storage[cmp.GetComponentType()].Add(cmp);
                // Add entity
                newArch.AddEntity(Entity);
                // Remove Entity from old
                Archetype.RemoveEntity(Entity);
                // Change archetype
                Archetype = newArch;
                
            }
            else
            {
                var aid = new ArchetypeID(comps.Select(c => c.GetComponentType()));

                var world = Entity.World;
                Archetype.RemoveEntity(Entity);
                Archetype = world.GenerateArchetype(aid, comps.Cast<ComponentBase>().ToList());
                Archetype.EntityID.Add(Entity.Index);
                foreach(var cmp in comps)
                {
                    Archetype.Storage[cmp.GetComponentType()].Add(cmp);
                }
                world.BuildGraph();
            }
        }
        public void Remove<T>() where T : struct
        {
            List<ComponentBase> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(ArchetypeIndex)).ToList();
            if(Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
            {
                // Add all components to new archetype
                foreach(var cmp in comps)
                    if(typeof(T) != cmp.GetComponentType())
                        newArch.Storage[cmp.GetComponentType()].Add(cmp);
                // Add entity
                newArch.AddEntity(Entity);
                // Remove Entity from old
                Archetype.RemoveEntity(Entity);
                // Change archetype
                Archetype = newArch;
            }
            else
            {
                var aid = new ArchetypeID(comps.Select(c => c.GetComponentType()).Where(ty => ty != typeof(T)));

                var world = Entity.World;
                Archetype.RemoveEntity(Entity);
                Archetype = world.GenerateArchetype(aid, comps.Where(c => c.GetComponentType() != typeof(T)).Cast<ComponentBase>().ToList());
                Archetype.EntityID.Add(Entity.Index);
                foreach(var cmp in comps.Where(ty => ty.GetComponentType() != typeof(T)))
                {
                    Archetype.Storage[cmp.GetComponentType()].Add(cmp);
                }
                world.BuildGraph();
            }
        }
        
    }
}