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