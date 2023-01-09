# SoftTouch.ECS

This project is a prototype for a lightweight ECS implementation with archetypal storage, heavily inspired by FLECS. It performs relatively well but there is room for improvement.

## FSharp example

The F# api is a work in progress, it doesn't cover all the C# one but it is designed to be very friendly to functional programming thanks to some dark type magic here and there.

```fsharp
open SoftTouch.ECS
open SoftTouch.ECS.FSharp
open SoftTouch.ECS.FSharp.ProcessorTypes

[<Struct>]
type NameComponent = 
    val mutable Name : string
    new (n : string) = {Name = n}
    override this.ToString() = $"{this.Name}"
    

let world = new World()

world 
|> World.CreateEntity
|> Entity.WithValue (NameComponent "Martha")
|> ignore


let nameSystem (entities1 : Query<NameComponent>) : unit =
    let mutable name = entities1[0].Component1
    printfn "%A" name
    name.Name <- "Jotaro Kujo"
    let v = entities1[0].Component1
    printfn "Changed to %A" v.Name



world
|> Processor.Add nameSystem
|> World.Start
|> World.Update
|> World.GetEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"

```

## Example

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
public class NameProcessor : Processor<Query<NameComponent>>
{
    public override void Update()
    {
        // query1 returns a list of entities with a copy of each components queried
        foreach((var e, var name) in query1)
        {
            e.Set(new NameComponent{Name = "Lola"});
        }
    }
}

public class ModelProcessor : Processor<Query<HealthComponent,ModelComponent>>
{
    public override void Update()
    {
        foreach((var e, var health, var model) in query1)
        {
            // Do something with model
        }
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
