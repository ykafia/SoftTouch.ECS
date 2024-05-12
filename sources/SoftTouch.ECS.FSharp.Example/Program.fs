open SoftTouch.ECS
open SoftTouch.ECS.FSharp
open SoftTouch.ECS.Querying
open SoftTouch.ECS.Scheduling
open SoftTouch.ECS.Attributes

[<Struct>]
type NameComponent = 
    val mutable Name : string
    new (n : string) = {Name = n}
    override this.ToString() = $"{this.Name}"
    

let app = new App()

[<Bundle("MyBundle")>]
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

let twoEntities (entities1 : Query<NameComponent>) (entities2 : Query<NameComponent>) : unit =
    for entity in entities1 do
        entity.Get<NameComponent>().Name
        |> printfn "original name is : %s"


        let name = NameComponent "Kujo Jolyne"
        entity.Set(&name)

        entity.Get<NameComponent>()
        |> printfn "Changed to %A"
    for entity in entities2 do
        entity.Get<NameComponent>().Name
        |> printfn "original name is : %s"


        let name = NameComponent "Kujo Jolyne"
        entity.Set(&name)

        entity.Get<NameComponent>()
        |> printfn "Changed to %A"

let x = 0;

app
|> App.addProcessor (Proc.from startup)
|> App.addProcessor (Proc.from nameSystem)
|> App.addProcessors [Proc.from nameSystem; Proc.from2 twoEntities]
|> App.addProcessorsTo (Main()) [
    Proc.from nameSystem
]
|> App.update 2
|> (fun app -> app.World)
|> World.getEntity 0
|> ignore
//|> fun x -> x.Name
//|> printfn "Hello %s"