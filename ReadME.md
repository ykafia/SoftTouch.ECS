# ECSharp

This project is a prototype for a lightweight ECS implementation with archetypal storage, heavily inspired by FLECS. It performs relatively well but there is room for improvement.

## FSharp example

The F# api is a work in progress, it doesn't cover all the C# one but it is designed to be very friendly to functional programming thanks to some dark type magic here and there.


```fsharp
open ECSharp
open ECSharp.FSharp
open ECSharp.FSharp.ProcessorTypes


[<Struct>]
type NameComponent = 
    val Name : string
    new (n : string) = {Name = n}
    

let world = new World()

world 
|> World.CreateEntity
|> Entity.WithValue (NameComponent "Martha")
|> ignore


let nameSystem (ents : Entities<NameComponent>) = 
    Entity.Set (NameComponent "John Doe") ents[0]


world
|> Processor.Add nameSystem

world.Update()

world 
|> World.GetEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"

```


## Example C#

Let's create a name and health component :

```csharp
public struct HealthComponent
{
    public float LifePoints;
    public float Shield;
}
public struct NameComponent
{
    public string Name;
}
public struct ModelComponent
{
    public byte[] Buffer;
}
```

As you can see, components are just structs.

Then we create a processor for `NameComponent` :

```csharp
public class NameProcessor : Processor<QueryEntity<NameComponent>>
{
    public override void Update()
    {
        // Query1 returns a list of archetypes containing the types
        // mentionned in the first QueryEntity of the processor's generics
        Query1
        .AsParallel()
        .ForAll(
            x => {
                for(int i = 0; i< x.Length; i++)
                    x.GetComponentArrayStruct<NameComponent>()[i] = 
                        new NameComponent{Name = "Lola2"};
            }
        );
    }
}

public class ModelProcessor : Processor<QueryEntity<ModelComponent>>
{
    public override void Update()
    {
        // Query1 returns a list of archetypes containing the types
        // mentionned in the first QueryEntity of the processor's generics
        Query1
        .AsParallel()
        .ForAll(
            x => {
                for(int i = 0; i< x.Length; i++)
                    x.GetComponentArray<ModelComponent>()[i].Buffer[5] = 1;
            }
        );
    }
}
```

And finally the code to create our world :

```csharp
var world = new World();

world.CreateEntity()
    .With(new NameComponent{Name = "Name"});
world.CreateEntity()
    .With(new NameComponent{Name = "Name2"})
    .With(new HealthComponent{})
    .With(new ModelComponent());

world.Add(new NameProcessor());
// After this line of code every NameComponent will be updated by the processor
world.Update();

```
