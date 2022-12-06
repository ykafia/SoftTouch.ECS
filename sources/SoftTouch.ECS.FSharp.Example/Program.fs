open SoftTouch.ECS
open SoftTouch.ECS.FSharp
open SoftTouch.ECS.FSharp.ProcessorTypes

[<Struct>]
type NameComponent = 
    val mutable Name : string
    new (n : string) = {Name = n}
    

let world = new World()

world 
|> World.CreateEntity
|> Entity.WithValue (NameComponent "Martha")
|> ignore


let nameSystem (a : World) (name : ref<NameComponent>) : unit =
    printfn "%A" name.Value.Name
    name.Value <- NameComponent("Jotaro Kujo")
    printfn "Changed to %A" name.Value.Name



world
|> Processor.Add nameSystem

for x in world.Processors
    do printfn "%A" x

world.Run(2)

world 
|> World.GetEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"