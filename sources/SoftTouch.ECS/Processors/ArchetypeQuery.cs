namespace SoftTouch.ECS;

public class ArchetypeQuery
{
    public IEnumerable<Archetype> Archetypes;

    public ArchetypeQuery(IEnumerable<Archetype> archetypes)
    {
        Archetypes = archetypes;
    }

    public void ProcessEntity1<T>(ref T comp)
    {
        
    }
}