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