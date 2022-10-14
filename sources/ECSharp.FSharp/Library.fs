namespace ECSharp.FSharp

// open ECSharp
// open ECSharp.Arrays

module ProcessorTypes =
    let x = 0;
    // type ComponentArrayInfo<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> = 
    //     {Arch : Archetype;  Array : ComponentList<'T>}

    

    // [<AbstractClass>]
    // type internal FProcessor() =
    //     inherit Processor()
    
    // type internal FProcessor1<'T when 
    //     'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType>
    //     (updater1 : EntityQuery<'T> -> unit) =
    //     inherit Processor<Query<'T>>()

    //     override this.Update() = 
    //         updater1 (EntityQuery<'T>)
        
//     //     type internal FSProcessor1<'T1, 'T2 when 
//     //     'T1 : struct and 'T1 : (new: unit -> 'T1) and 'T1 :>System.ValueType and
//     //     'T2 : struct and 'T2 : (new: unit -> 'T2) and 'T2 :>System.ValueType>
//     //     (updater1 : seq<Archetype> -> unit) =
//     //     inherit Processor<Query<'T1, 'T2>>()

//     //     member this.Query1 = new Query<'T1,'T2>()

//     //     override this.Update() = 
//     //         updater1 (Seq.cast<Archetype> this.Query1.QueriedArchetypes)
    
//     // type internal FSProcessor<'Q1, 'T, 'U when 'Q1 :> Query and 'Q1 :> seq<ArchetypeRecord * 'T * 'U>
//     //         and 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType
//     //         and 'U : struct and 'U : (new: unit -> 'U) and 'U :>System.ValueType>
//     //         (updater1 : (seq<ArchetypeRecord * 'T * 'U> -> unit)) =
//     //     inherit FSProcessor()

//     //     member this.Query1 = new Query(this.World) :?> 'Q1

//     //     override this.Update() = 
//     //         updater1 this.Query1


// module World =
//     let CreateEntity (world : World) =
//         world.CreateEntity()
//     let GetEntity  (index : int64) (world : World)=
//         world[index]

// module Entity =
//     let With<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (builder : EntityBuilder) =
//         builder.With<'T>()
    
//     let WithValue<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (c : 'T) (builder : EntityBuilder) =
//         builder.With(&c)
    
//     let Get<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (arch : ArchetypeRecord) =
//         arch.Get<'T>()
//     let Set<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (cmp : 'T) (arch : ArchetypeRecord) =
//         arch.Set<'T>(cmp)
//     let Has<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> (arch : ArchetypeRecord) =
//         arch.Get<'T>()

// module Processor =

//     open ProcessorTypes

//     let Add<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (updater : seq<ArchetypeRecord * 'T> -> unit) (world : World) =
//         world.Add(new FSProcessor1<'T>(updater))
    
//     let Add2<'T1, 'T2 when 'T1 : struct and 'T1 : (new: unit -> 'T1) and 'T1 :>System.ValueType
//             and 'T2 : struct and 'T2 : (new: unit -> 'T2) and 'T2 :>System.ValueType> (updater : seq<ArchetypeRecord * 'T1 * 'T2> -> unit) (world : World) =
//         world.Add((new FSProcessor1<'T1, 'T2>(updater)))


// module Archetype =

//     let GetArray<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (arch : Archetype) = 
//         arch.GetComponentSpan<'T>()
    
//     let SetComponent<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (index : int) (comp : 'T) (arch : Archetype) = 
//         arch.SetComponent(index,&comp)

    
