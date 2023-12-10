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
    |> Commands.WithValue (NameComponent "No Name")
    |> ignore
    

let nameSystem (entities : Query<NameComponent>) : unit =
    for entity in entities do
        entity.Get<NameComponent>().Name
        |> printfn "original name is : %s"


        let name = NameComponent "Kujo Jolyne"
        entity.Set(&name)

        entity.Get<NameComponent>()
        |> printfn "Changed to %A"

let x = 0;

app
|> Processor.AddStartup startup
|> Processor.Add nameSystem
|> App.update
|> App.update
|> (fun app -> app.World)
|> World.getEntity 0 
|> Entity.Get<NameComponent>
//|> fun x -> x.Name
//|> printfn "Hello %s"