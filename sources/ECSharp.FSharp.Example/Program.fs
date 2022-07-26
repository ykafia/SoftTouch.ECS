open ECSharp.FSharp
open ECSharp

type NameComponent = 
    struct
        val Name : string
        new (n : string) = {Name = n}
    end

let w = new World()

w 
|> World.CreateEntity
|> Entity.WithValue (NameComponent "Martha")
|> ignore


let updaterFunc (archs : seq<Archetype>) = 
    ()


w
|> Processor.Add (Processor.Create updaterFunc)

w 
|> World.GetEntity 0 
|> Entity.Get<NameComponent>
|> fun x -> x.Name
|> printfn "Hello %s"