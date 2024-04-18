# Design

Each processor should be gathered into stages. Those stages are important to separate concerns like polling inputs before running the game logic and physics engine.

A stage will be a pool of processors. Each stage will be run sequentially, each processors in the stage will be run in parallel unless there is ordering applied or processors need write access on the same archetypes.


## A graph for stages

Each stage will be declared with an order of execution making sure stages 



# Adding processor to the world


```csharp
var app = 
    new App()
    .AddProcessors<Startup,Groups<Processors<SpawnEntities, InitiateValues>>>()

```