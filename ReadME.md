# SoftTouch.ECS

This project is lightweight ECS implementation with archetypal storage, heavily inspired by FLECS.
The API aims to be very fast and allocation free for queries and component updates.

## Usage

At the moment the library is not distributed on nuget so you still have to pull it from git and reference it in your project.

The api is very similar to [bevy-ecs](https://bevyengine.org) but the naming conventions is inspired from [Stride3D](https://stride3d.net)'s.


### API

#### World and entities

Everything starts with the creation of a `World` object. Worlds manage their entities, storages for components and processors. 

```csharp
var world = new World();
```

Once you have a world you can spawn entities either with components or without.

```csharp
world.Spawn();
world.Spawn(new Name("John Doe"), default(Transform), (1,"some text"));
world
    .Spawn()
    .With(Name("Jane doe"))
    .With(("mochi",5,true));
```

#### Components

Components are stored in `List<T>`. To make sure components are not allocated individually on the heap, they are constrained to be structs.
This both avoids fragmentation and make sure iterating over them is made very fast. 

Using the code above we could have defined our components this way :

```csharp
public struct Name
{
    public string Value { get; init; }

    public Name(string name){ Value = name };

    public static implicit operator string(Name n) => n.Value
}


public struct Transform
{
    public Vector3 Position { get; init; }
    public Quaternion Rotation { get; init; }
    public Vector3 Scale { get; init; }

    public Transform(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Position = pos;
        Rotation = rot;
        Scale = scale;
    }
}
```

#### Systems/Processors

In this library, the S in ECS has been renamed to `Processor`, this was a choice influenced by Stride's naming convention and also because I personally feel System is very vague for what this implementation is really.

To create Systems/Processors you can either create it from a class implementation

```csharp
public class MyFirstProcessor : Processor<Query<Name,Transform>>
{
    public override void Update()
    {
        // Here goes your logic
    }
}
```

Or create a static function

```csharp

public static void MyFirstSystem(Query<Name,Transform> query1)
{
    // Here goes your logic
}

```

And then add it to the world and you can start updating your frames!

```csharp
world.AddProcessor<MyFirstProcessor>();
// world.AddProcessor(new MyFirstProcessor());

world.AddProcessor(
    (Query<Name,Transform> q1) => MyFirstSystem(q1)
);

// Sometimes you can add processors that are called only once in the beginning before anything
// For this, the Start function was created
world.Start();


for(int i = 0; i < 100; i++)
    world.Update();
```


#### Iterating over entities

This is the meat and potatoes of this library. Iterating over entities can be done in many different ways but for that there needs to be an explanation about how component storage works in this library.

Entities are just indices linked to `Archetype`s. They are stored in the world as a list.

Entities are grouped together based on which group components they hold. Those groups are called `Archetype`s. 
When you spawn an entity with components, the world checks if this entity can fit in an existing `Archetype` or has to generate a new one.
You can also add a component to an entity and the world will wait for the end of a frame to move the entity to another `Archetype` and add the corresponding component data.

`Archetype`s contain a `Storage` value which has a type ressembling `Dictionary<Type,List<T>>` and an index redirection to tell which `Archetype` index corresponds to which entity index.

As a bare bone implementation, you could iterate over those `Archetype`s yourself and select which entities you want to work with like so :

```csharp
public class MyFirstProcessor : Processor<Query<Name,Transform>>
{
    public override void Update()
    {
        // Processors created with a class have access to the world, so you can access pretty much any storage from there
        World.Archetypes.Values[0].Storage[typeof(Name)][0] = new Name("Ada Lovelace");
    }
}
```

But when you want to iterate over many entities and avoid allocation, the type `Query<T1,T2,..., T7>` offers an enumerator to help you iterate over entities containing components you specified in the generics.
And thanks to the power of `ref struct`s and duck typing, you can use `foreach` without worrying about allocation or speed. The enumerator is made in the same way of `Span<T>`'s enumerator, is allocation free and tries to be as fast as possible.

```csharp
// Here the processor queries over entities that have a Name and Transform components
public class MyFirstProcessor : Processor<Query<Name,Transform>>
{
    public override void Update()
    {
        // Entities1 is the Query object, it basically queries entities
        // The 1 is because you can have up to 4 queries in a processor if you want to iterate over two different list of entities
        // e.g. an entity with a mesh component and another with a camera component
        foreach(var entity in Entities1)
        {
            // The entity here can be deconstructed into the components queried
            var (name, transform) = entity;
            // entity also has a method to set a component. It can be one you queried, or another that you know exists but haven't queried
            // There also is a Get<T> method as well as a Has<T> method to help you make safe code
            if(name == "John Doe")
                entity.Set(new Name("Ada Lovelace"));
        }
    }
}
```


### Future of the API

So far the query api has been stabilized, i don't think it'll change much more. 
My focus will shift on the `System`/`Processor` scheduler, to make sure processors can be run on parallel without too much data race.


## F# API

The F# api covers a subset of the C# api but it is designed to be very friendly to functional programming.

Here's an example :

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
|> World.Spawn
|> EntityCommands.WithValue (NameComponent "Martha")
|> ignore


let nameSystem (entities1 : Query<NameComponent>) : unit =
    for entity in entities1 do
        let name = NameComponent "Kujo Jolyne"
        entity.Set(&name)

        entity.Get<NameComponent>()
        |> printfn "Changed to %A"



world
|> Processor.Add nameSystem
|> World.Start
|> World.Update
|> World.GetEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"

```