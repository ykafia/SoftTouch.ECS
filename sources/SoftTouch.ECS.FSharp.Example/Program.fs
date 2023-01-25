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