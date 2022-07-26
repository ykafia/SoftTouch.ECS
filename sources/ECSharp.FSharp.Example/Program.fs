open ECSharp.FSharp
open ECSharp

// For more information see https://aka.ms/fsharp-console-apps

type NameComponent = 
    struct
        val Name : string
        new (n : string) = {Name = n}
    end

let w = new World()

let martha = NameComponent("Martha")

w 
|> World.CreateEntity
|> Entity.WithValue (NameComponent "Martha")
|> ignore

w
|> Processor.Add (Processor.Create (fun _ -> () ))

w 
|> World.GetEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"