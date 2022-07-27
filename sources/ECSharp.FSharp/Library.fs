namespace ECSharp.FSharp

open ECSharp
open ECSharp.Arrays

module ProcessorTypes =
    type ComponentArrayInfo<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> = 
        {Arch : Archetype;  Array : ComponentList<'T>}


    type Entities<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType>(archs : seq<Archetype>, world : World) =
        class
            let mutable archs = archs
            let world = world

            member this.Item
                with get(i : int64) = world[i]


            member this.Archs with get() = archs and set(value) = archs <- value
            member this.Components1 = this.Archs |> Seq.map (fun a -> {Arch = a; Array = a.GetComponentArray<'T>()})
        end
    type Entities<
            'T, 'U when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType
            and 'U : struct and 'U : (new: unit -> 'U) and 'U :>System.ValueType>(archs : seq<Archetype>, world : World) =
        inherit Entities<'T>(archs, world)
        member this.Components2 = this.Archs |> Seq.map (fun a -> {Arch = a; Array = a.GetComponentArray<'U>()})
    type Entities<
            'T, 'U, 'V when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType
            and 'U : struct and 'U : (new: unit -> 'U) and 'U :>System.ValueType
            and 'V : struct and 'V : (new: unit -> 'V) and 'V :>System.ValueType>(archs : seq<Archetype>, world : World) =
        inherit Entities<'T, 'U>(archs, world)
        member this.Components3 = this.Archs |> Seq.map (fun a -> {Arch = a; Array = a.GetComponentArray<'V>()})


    [<AbstractClass>]
    type internal FSProcessor() =
        inherit Processor()
    
    type internal FSProcessor<'Q1, 'T when 'Q1 :> IQueryEntity and 'Q1 : (new: unit -> 'Q1)
            and 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType>(updater1 : (Entities<'T> -> unit)) =
        inherit FSProcessor()

        member this.queryEntity = new 'Q1()

        member this.Query1 = this.World.QueryArchetypes(this.queryEntity.GetQueryType())

        override this.Update() = 
            updater1 (new Entities<'T>(this.Query1, this.World))
    
    type internal FSProcessor<'Q1, 'T, 'U when 'Q1 :> IQueryEntity and 'Q1 : (new: unit -> 'Q1)
            and 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType
            and 'U : struct and 'U : (new: unit -> 'U) and 'U :>System.ValueType>(updater1 : (Entities<'T, 'U> -> unit)) =
        inherit FSProcessor()

        member this.queryEntity = new 'Q1()

        member this.Query1 = this.World.QueryArchetypes(this.queryEntity.GetQueryType())

        override this.Update() = 
            updater1 (new Entities<'T,'U>(this.Query1, this.World))


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
    let Set<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (cmp : 'T) (arch : ArchetypeRecord) =
        arch.Set<'T>(cmp)
    let Has<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (arch : ArchetypeRecord) =
        arch.Get<'T>()

module Processor =

    open ProcessorTypes

    let Add<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (updater : Entities<'T> -> unit) (world : World) =
        world.Add(new FSProcessor<QueryEntity<'T>, 'T>(updater))
    
    let Add2<'T, 'U when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType
            and 'U : struct and 'U : (new: unit -> 'U) and 'U :>System.ValueType> (updater : Entities<'T, 'U> -> unit) (world : World) =
        world.Add((new FSProcessor<QueryEntity<'T,'U>, 'T, 'U>(updater)))


module Archetype =

    let GetArray<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (arch : Archetype) = 
        arch.GetComponentArray<'T>()
    
    let SetComponent<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (index : int) (comp : 'T) (arch : Archetype) = 
        arch.SetComponent(index,&comp)

    
