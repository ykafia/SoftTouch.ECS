module Systems

open Components
open SoftTouch.ECS.Querying

let machinSystem (query : Query<Machin, NoFilter>) =
    for e in query do
        let machin = Machin(1)
        e.Set<Machin>(&machin)