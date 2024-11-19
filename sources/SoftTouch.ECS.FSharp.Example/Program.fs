open SoftTouch.ECS
open SoftTouch.ECS.FSharp
open SoftTouch.ECS.Querying
open SoftTouch.ECS.Scheduling
open SoftTouch.ECS.Attributes


type RenderApp(parent) =
    inherit SubApp(parent)


type RenderStage() =
    inherit SubStage<Main>()


[<Struct>]
type NameComponent = 
    val mutable Name : string
    new (n : string) = {Name = n}
    override this.ToString() = $"{this.Name}"
    
let startup (commands : Commands) =
    commands
    |> Commands.spawn
    |> Commands.WithValue (NameComponent "No Name")
    |> ignore
    

let nameSystem (entities : Query<NameComponent, NoFilter>) : unit =
    for entity in entities do
        entity.Get<NameComponent>().Name
        |> printfn "original name is : %s"
        let name = NameComponent "Kujo Jolyne"
        entity.Set(&name)

        entity.Get<NameComponent>()
        |> printfn "Changed to %A"

let twoEntities (entities1 : Query<NameComponent, NoFilter>) (entities2 : Query<NameComponent, NoFilter>) (commands : Commands) : unit =
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

[<EntryPoint>]
let main argv =
    let app = new App()
    
    app
    |> App.addStartupProcessor (Proc.from startup None)
    |> App.addProcessor (Proc.from nameSystem None)
    |> App.update 5
    |> ignore

    printfn "%A" app.World[0]

    0