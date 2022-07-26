namespace ECSharp.FSharp

open ECSharp

module World =
    let CreateEntity (world : World) =
        world.CreateEntity()
    let GetEntity  (index : int64) (world : World)=
        world[index]

module Entity =
    let With<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (builder : EntityBuilder) =
        builder.With<'T>()
    
    let WithValue<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (c : 'T) (builder : EntityBuilder) =
        builder.With(&c)
    
    let Get<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (arch : ArchetypeRecord) =
        arch.Get<'T>()
    let Has<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (arch : ArchetypeRecord) =
        arch.Get<'T>()

module Processor =

    [<AbstractClass>]
    type FSProcessor() =
        inherit Processor()
    
    type FSProcessor<'Q1 when 'Q1 :> IQueryEntity and 'Q1 : (new: unit -> 'Q1)>(updater1 : (seq<Archetype> -> unit)) =
        inherit FSProcessor()

        member this.queryEntity = new 'Q1()

        member this.Query1 = this.World.QueryArchetypes(this.queryEntity.GetQueryType())

        override this.Update() = this.Query1 |> updater1

    let Create<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (updater : seq<Archetype> -> unit) =
        new FSProcessor<QueryEntity<'T>>(updater)

    let Add (processor : FSProcessor) (world : World) =
        world.Add(processor)

    
