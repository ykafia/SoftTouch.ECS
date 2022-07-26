open ECSharp.FSharp
open ECSharp
open System.Runtime.CompilerServices

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
|> Entity.WithValue<NameComponent> (NameComponent "Martha")
|> ignore

printfn "Hello %s" (w |> World.GetEntity 0 |> Entity.Get<NameComponent>).Name
