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