# SoftTouch.ECS

This project is lightweight ECS implementation with archetypal storage, heavily inspired by FLECS.
The API aims to be very fast and allocation free for queries and component updates.

## Usage

At the moment the library is not distributed on nuget so you still have to pull it from git and reference it in your project.

The api is very similar to [bevy-ecs](https://bevyengine.org) but the naming conventions is inspired from [Stride3D](https://stride3d.net)'s.


### API

#### World and entities

Everything starts with the creation of a `World` object. Worlds manage their entities, storages for components and processors. You usually use it through the `App` object.

```csharp
var app = new App();
```

Once you have a world you can spawn entities either with components or without.

```csharp
app.World.Commands.Spawn();
app.World.Commands.Spawn(new Name("John Doe"), default(Transform), (1,"some text"));
app.World.Commands
    .Spawn()
    .With(Name("Jane doe"))
    .With(("mochi",5,true));
```

#### Components

Components are stored in `List<T>`. To make sure components are not allocated individually on the heap, they are constrained to be structs.
This both avoids fragmentation and make sure iterating over them is made very fast. 

Using the code above we could have defined our components this way :

```csharp
public record struct Name(string Value)
{
    public static implicit operator string(Name n) => n.Value
}


public record struct Transform(Vector3 Position, Quaternion Rotation, Vector3 Scale);


```

#### Systems/Processors

In this library, the S in ECS has been renamed to `Processor`, this was a choice influenced by Stride's naming convention and also because I personally feel System is very vague for what this implementation is really.

To create Systems/Processors you can either create it from a class implementation

```csharp


public class MyStartupProcessor : Processor<Commands, Resource<MyResource>>
{
    public override void Update()
    {
        var commands = Query1;
        commands.Spawn(new NameComponent("Jane doe"), 5, new HealthComponent(100,100));
        commands.Spawn();
    }
}

public class MyFirstProcessor : Processor<Query<Read<Name>,Write<Transform>>>
{
    public override void Update()
    {
        foreach(var e in Query)
        {
            // Here goes your logic
        }
    }
}
```

Or create a static function

```csharp

public static void MyFirstSystem(Query<Write<Name,Transform>> entities)
{
    // Here goes your logic
}

```

And then add it to the world and you can start updating your frames!

```csharp
app.AddProcessor<MyFirstProcessor>();
app.AddStartupProcessor<MyStartupProcessor>();
// world.AddProcessor(new MyFirstProcessor());

app.AddProcessor(
    (Query<Read<Name,Transform>> q1) => MyFirstSystem(q1)
);

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
public class MyFirstProcessor : Processor<Query<Read<Name,Transform>>>
{
    public override void Update()
    {
        // Processors created with a class have access to the world, so you can access pretty much any storage from there
        World.Archetypes.Values[0].Storage[typeof(Name)][0] = new Name("Ada Lovelace");
    }
}
```

This is a very versatile way of querying the world, you get to the data you need, but it's mouthful and not really good for performances.

Processors have Query fields that contains helper methods and iterators to help you iterate over entities and their components. When using iterators you constrain your logic to the types you have chosen to work with. This makes it easier for the system to avoid processors accessing the same chunks of memory at the same time, to avoid cache misses.


```csharp
// Here the processor queries over entities that have a Name and Transform components
public class MyFirstProcessor : Processor<Query<Read<Name,Transform>>>
{
    public override void Update()
    {
        // Query is a field helping you with iterators
        // The 1 is because you can have up to 4 queries in a processor if you want to iterate over two different list of entities
        // e.g. an entity with a mesh component and another with a camera component
        foreach(var entity in Query)
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
open SoftTouch.ECS.Querying

[<Struct>]
type NameComponent = 
    val mutable Name : string
    new (n : string) = {Name = n}
    override this.ToString() = $"{this.Name}"
    

let app = new App()


let startup (commands : Commands) =
    commands
    |> Commands.spawn
    |> Commands.WithValue (NameComponent "hello")
    |> ignore
    

let nameSystem (entities : Query<Read<NameComponent>>) : unit =
    for entity in entities do
        entity.Get<NameComponent>().Name
        |> printfn "original name is : %s"


        let name = NameComponent "Kujo Jolyne"
        entity.Set(&name)

        entity.Get<NameComponent>()
        |> printfn "Changed to %A"



app
|> Processor.AddStartup startup
|> Processor.Add nameSystem
|> App.update
|> (fun app -> app.World)
|> World.getEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"

```