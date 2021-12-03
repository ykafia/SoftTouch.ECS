using System.Collections.Generic;
using System.Linq;

namespace WonkECS
{
    public class ArchetypeRecord
    {
        public Entity Entity;
        public Archetype Archetype;
        public int ArchetypeIndex;

        public override string ToString()
        {
            return Archetype.ToString();
        }

        public void Set<T>(T cmp) where T : struct
            => Archetype.SetComponent(ArchetypeIndex, cmp);
        
        public T Get<T>() where T : struct
            => Archetype.GetComponentArray<T>()[ArchetypeIndex];
        
        public bool Has<T>() where T : struct
            => Archetype.Storage.ContainsKey(typeof(T));

        public void Remove<T>() where T : struct
        {
            var id = ArchetypeIndex;
            List<ComponentBox> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(id)).Where(x => x.GetComponentType() != typeof(T)).ToList();
            if(Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
            {
                // Step 1 add all components to new archetype
               
                Archetype.RemoveEntityIndex(id);
                newArch.EntityID.Add(Entity.Index);
                ArchetypeIndex = newArch.EntityID.Count;
                Archetype = newArch;
                foreach(var cmp in comps)
                {
                    newArch.Storage[cmp.GetComponentType()].Add(cmp);
                }
            }
            else
            {
                var aid = new ArchetypeID(Archetype.Storage.Keys.Where(x => x != typeof(T)));

                var world = Entity.World;
    
                Archetype = world.GenerateArchetype(aid, comps);
                Archetype.EntityID.Add(Entity.Index);
                foreach(var cmp in comps)
                {
                    Archetype.Storage[cmp.GetComponentType()].Add(cmp);
                }
                world.BuildGraph();
            }
        }
    }
}