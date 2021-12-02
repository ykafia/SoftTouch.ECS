using System.Collections.Generic;
using System.Linq;

namespace WonkECS
{
    public struct ArchetypeRecord
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
        
        public bool Has<T>() where T : struct
            => Archetype.Storage.ContainsKey(typeof(T));

        public void Remove<T>() where T : struct
        {
            var aid = new ArchetypeID(Archetype.Storage.Keys.Where(x => x != typeof(T)));
            if(Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
            {
                // Step 1 add all components to new archetype
                var id = ArchetypeIndex;
                List<ComponentBox> comps = Archetype.Storage.Values.Select(x => x.RemoveAt(id)).ToList();
                Archetype.RemoveEntityIndex(id);
                newArch.EntityID.Add(Entity.Index);
                ArchetypeIndex = newArch.EntityID.Count;
                Archetype = newArch;
                foreach(var cmp in comps)
                {
                    if(cmp.GetComponentType() != typeof(T))
                        newArch.Storage[cmp.GetComponentType()].Add(cmp);
                }
            }
        }
    }
}