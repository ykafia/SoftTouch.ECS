namespace SoftTouch.ECS.FSharp

open SoftTouch.ECS
open SoftTouch.ECS.Arrays
open FSharp.Core
open System
open System.Runtime.CompilerServices

module ProcessorTypes =
    
    type UpdaterDel<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> 
        = delegate of World * byref<'T> -> unit
    type UpdaterDel<'T1, 'T2 when 'T1 : struct and 'T1 : (new: unit -> 'T1) and 'T1 :> System.ValueType
                    and 'T2 : struct and 'T2 : (new: unit -> 'T2) and 'T2 :> System.ValueType
                    > 
        = delegate of World * byref<'T1> * byref<'T2> -> unit
    type SimpleFProcessor<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> 
        (updater : UpdaterDel<'T>) =
        inherit Processor()
        member this.Updater = updater
        member this.aid = new ArchetypeID(typeof<'T>)
        override this.Update() =
            for arch in this.World.QueryArchetypes(this.aid)
                do
                for i in 0..arch.Length
                    do 
                    this.Updater.Invoke(this.World, &arch.GetComponentSpan<'T>().[i])
        
    type SimpleFProcessor<
            'T1,'T2 when 'T1 : struct and 'T1 : (new: unit -> 'T1) and 'T1 :> System.ValueType
                     and 'T2 : struct and 'T2 : (new: unit -> 'T2) and 'T2 :> System.ValueType> 
        (updater : UpdaterDel<'T1,'T2>) =
        inherit Processor()
        let Updater = updater

        let aid = new ArchetypeID(typeof<'T1>,typeof<'T2>)
        override this.Update() =
            for arch in this.World.QueryArchetypes(aid)
                do
                for i in 0..arch.Length
                    do 
                        Updater.Invoke(this.World, &arch.GetComponentSpan<'T1>().[i],&arch.GetComponentSpan<'T2>().[i])
        
    let createSimple<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> 
        (updateFunction : World -> ref<'T> -> unit) = 
            let func = new SimpleUpdate<'T>(fun x y -> updateFunction x (ref y))
            new SimpleProcessor<'T>(func)
    
    let createSimple2<'T1, 'T2 when 'T1 : struct and 'T1 : (new: unit -> 'T1) and 'T1 :> System.ValueType
                                and 'T2 : struct and 'T2 : (new: unit -> 'T2) and 'T2 :> System.ValueType
                    > 
        (updateFunction : World * 'T1 * 'T2 -> unit) = 
            let func = new SimpleUpdate<'T1,'T2>(fun x a1 a2-> updateFunction(x,a1,a2))
            new SimpleProcessor<'T1,'T2>(func)

    // [<Struct>]
    // type MyStruct =
    //     struct
    //         val a : int
    //     end

    // let testUpdate (w : World, i : MyStruct) =
    //     ()

    // let instanciateDelegate<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType> 
    //     a =
    //     new SimpleUpdate<'T>(fun x y -> a(x,y)) 

    
    // let simp = 
    //     createSimple testUpdate





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

    let Add<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> 
        (updater : World -> ref<'T> -> unit) (world : World) =
        world.Add(createSimple updater)
    
    let Add2<'T1, 'T2 when 'T1 : struct and 'T1 : (new: unit -> 'T1) and 'T1 :>System.ValueType
            and 'T2 : struct and 'T2 : (new: unit -> 'T2) and 'T2 :>System.ValueType> 
            (updater : World * 'T1 * 'T2 -> unit) (world : World) =
        world.Add(createSimple2 updater)


module Archetype =

    let GetArray<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (arch : Archetype) = 
        arch.GetComponentSpan<'T>()
    
    let SetComponent<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (index : int) (comp : 'T) (arch : Archetype) = 
        arch.SetComponent(index,&comp)

    
