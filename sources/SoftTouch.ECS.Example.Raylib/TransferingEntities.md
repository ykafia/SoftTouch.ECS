# Transfering entities

An entity would typically be transfered from MainApp to the RenderApp.

```
Main                    Render
Entity[0,0]     ->      -----------
Entity[1,0]     ->      Entity[1,0]
```

Entities in RenderApp would be sparser then MainApp but that's not an issue since each components would still likely be in the same archetypes.

## Remarks

1. If a component is extracted from MainApp to RenderApp and the corresponding entity has the component, it should be modified in place
2. If a component is extracted and was not existing before, it should create an EntityUpdate
3. If a component is removed, the entity should move into another archetype.
4. A higher level rule would be : If an entity has switched archetypes, the process of updating should be first to modify the similar components and then to use EntityUpdate
5. There is a possibility for the entity to be deleted, the RenderApp should flush it too.
6. At the end of the rendering, the entities should be flushed, every archetypes should exist without entities.
