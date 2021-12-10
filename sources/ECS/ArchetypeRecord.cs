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
            => Archetype.GetComponentArrayStruct<T>()[ArchetypeIndex];
        
        public T GetRef<T>() where T : Component
            => Archetype.GetComponentArray<T>()[ArchetypeIndex];
        
        public bool Has<T>()
            => Archetype.Storage.ContainsKey(typeof(T));


        public void Add<T>(in T c) where T : struct
        {
            List<ComponentStruct<T>> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(ArchetypeIndex)).Cast<ComponentStruct<T>>().ToList();
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
    
                Archetype = world.GenerateArchetype(aid, comps.Cast<ComponentBase>().ToList());
                Archetype.EntityID.Add(Entity.Index);
                foreach(var cmp in comps)
                {
                    Archetype.Storage[cmp.GetComponentType()].Add(cmp);
                }
                world.BuildGraph();
            }
        }

        public void Add<T>(T c) where T : Component
        {
            List<Component> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(ArchetypeIndex)).Cast<Component>().ToList();
            comps.Add(c);
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
                var aid = new ArchetypeID(comps.Select(c => c.GetType()));

                var world = Entity.World;
    
                Archetype = world.GenerateArchetype(aid, comps.Cast<ComponentBase>().ToList());
                Archetype.EntityID.Add(Entity.Index);
                foreach(var cmp in comps)
                {
                    Archetype.Storage[cmp.GetComponentType()].Add(cmp);
                }
                world.BuildGraph();
            }
        }


        public void Remove<T>()
        {
            if(typeof(T).IsValueType)
                RemoveStruct<T>();
            else
            {
                List<Component> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(ArchetypeIndex)).Cast<Component>().Where(x => x.GetComponentType() != typeof(T)).ToList();
                if(Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
                {
                    // Add all components to new archetype
                    foreach(var cmp in comps)
                        newArch.Storage[cmp.GetComponentType()].Add(cmp);
                    // Add entity to new archetypy
                    newArch.AddEntity(Entity);
                    // Remove Entity on old archetype
                    Archetype.RemoveEntity(Entity);
                    // Change Archetype
                    Archetype = newArch;
                }
                else
                {
                    var aid = new ArchetypeID(Archetype.Storage.Keys.Where(x => x != typeof(T)));

                    var world = Entity.World;
        
                    var genArch = world.GenerateArchetype(aid, comps.Cast<ComponentBase>().ToList());
                    genArch.AddEntity(Entity);
                    foreach(var cmp in comps)
                        genArch.Storage[cmp.GetComponentType()].Add(cmp);
                    Archetype.RemoveEntity(Entity);
                    Archetype = genArch;
                    world.BuildGraph();
                }
            }
            
            
        }
        public void RemoveStruct<T>()
        {
        }
        
    }
}