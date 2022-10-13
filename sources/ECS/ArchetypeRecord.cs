using System.Collections.Generic;
using System.Linq;
using ECSharp.Arrays;
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
            Entity.World.AddArchetypeUpdate(new ComponentAdd<T>(c, this));
        }
        public void Remove<T>() where T : struct
        {
            Entity.World.AddArchetypeUpdate(new ComponentRemove<T>(this));
        }

        internal void AddComponent<T>(in T c) where T : struct
        {
            var arrays = Archetype.Storage;
            
            if(Archetype.Edges.Add.TryGetValue(typeof(T), out var newArch))
            {
                // Add all components to new archetype
                foreach(var cmp in arrays)
                {
                    cmp.Value.TransferTo(newArch.Storage[cmp.Key], ArchetypeIndex);
                }
                // Add entity
                newArch.AddComponent(c,Entity.Index);
                // Remove Entity from old
                Archetype.RemoveEntity(Entity);
                // Change archetype
                Archetype = newArch;
                
            }
            else
            {
                var aid = new ArchetypeID(arrays.Keys.Append(typeof(T)).ToHashSet());

                var world = Entity.World;
                var arch = world.GenerateArchetype(aid, arrays.Values.Append(new ComponentList<T>()));
                arch.AddComponent(c, Entity.Index);
                foreach(var cmp in arrays)
                    cmp.Value.TransferTo(arch.Storage[cmp.Key],ArchetypeIndex);
                
                Archetype.RemoveEntity(Entity);
                Archetype = arch;
                world.BuildGraph();
            }
        }
        internal void RemoveComponent<T>() where T : struct
        {
            var arrays = Archetype.Storage;
            if(Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
            {
                // Add all components to new archetype
                foreach(var cmp in arrays.Where(x => x.Key != typeof(T)))
                    cmp.Value.TransferTo(newArch.Storage[cmp.Key], ArchetypeIndex);
                // Add entity
                newArch.AddEntity(Entity);
                // Remove Entity from old
                Archetype.RemoveEntity(Entity);
                // Change archetype
                Archetype = newArch;
            }
            else
            {
                var aid = new ArchetypeID(arrays.Select(c => c.Key).Where(ty => ty != typeof(T)).ToHashSet());

                var world = Entity.World;
                var arch = world.GenerateArchetype(aid, arrays.Where(c => c.Key != typeof(T)).Select(x => x.Value));
                arch.AddEntity(Entity);
                foreach(var cmp in arrays.Where(x => x.Key != typeof(T)))
                    cmp.Value.TransferTo(arch.Storage[cmp.Key], ArchetypeIndex);
                Archetype.RemoveEntity(Entity);
                Archetype = arch;
                world.BuildGraph();
            }
        }
        
    }
}