namespace SoftTouch.ECS.FSharp

open SoftTouch.ECS
open SoftTouch.ECS.Arrays
open FSharp.Core
open System
open System.Runtime.CompilerServices

module ProcessorTypes =

    type FProcessor<'Q1 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1)>
        (updater : 'Q1 -> unit) =
        inherit Processor<'Q1>()
        override this.Update() = 
            updater this.Entities1
    
    let createProcessor1<'Q1 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1)> 
        (updater : 'Q1 -> unit) = 
        FProcessor<'Q1>(updater)
    
    type FProcessor<'Q1, 'Q2 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2)> 
        (updater : 'Q1 -> 'Q2 -> unit) =
        inherit Processor<'Q1,'Q2>()
        override this.Update() = 
            updater this.Entities1 this.Entities2
    
    let createProcessor2<'Q1, 'Q2 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2)> 
            (updater : 'Q1 -> 'Q2 -> unit) = 
        FProcessor<'Q1,'Q2>(updater)
    
    type FProcessor<'Q1, 'Q2, 'Q3 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2) and 'Q3 :> Query and 'Q3 : (new: unit -> 'Q3)> 
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> unit) =
        inherit Processor<'Q1,'Q2, 'Q3>()
        override this.Update() = 
            updater this.Entities1 this.Entities2 this.Entities3
    
    let createProcessor3<'Q1, 'Q2, 'Q3 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2) and 'Q3 :> Query and 'Q3 : (new: unit -> 'Q3)> 
            (updater : 'Q1 -> 'Q2 -> 'Q3 -> unit) = 
        FProcessor<'Q1,'Q2, 'Q3>(updater)
    
    type FProcessor<'Q1, 'Q2, 'Q3, 'Q4 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2) and 'Q3 :> Query and 'Q3 : (new: unit -> 'Q3) and 'Q4 :> Query and 'Q4 : (new: unit -> 'Q4)> 
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4 -> unit) =
        inherit Processor<'Q1,'Q2,'Q3,'Q4>()
        override this.Update() = 
            updater this.Entities1 this.Entities2 this.Entities3 this.Entities4
    
    let createProcessor4<'Q1, 'Q2, 'Q3, 'Q4 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2) and 'Q3 :> Query and 'Q3 : (new: unit -> 'Q3) and 'Q4 :> Query and 'Q4 : (new: unit -> 'Q4)> 
            (updater : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4 -> unit) = 
        FProcessor<'Q1,'Q2,'Q3,'Q4>(updater)

    




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

    let Add<'Q1 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1)>
        (updater : 'Q1 -> unit) (world : World) =
        world.Add(createProcessor1 updater)
    
    let Add2<'Q1, 'Q2 when 'Q1 :> Query and 'Q1 : (new: unit -> 'Q1) and 'Q2 :> Query and 'Q2 : (new: unit -> 'Q2)> 
        (updater : 'Q1 -> 'Q2 -> unit) (world : World) =
        world.Add(createProcessor2 updater)


module Archetype =

    let GetArray<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (arch : Archetype) = 
        arch.GetComponentSpan<'T>()
    
    let SetComponent<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :>System.ValueType> (index : int) (comp : 'T) (arch : Archetype) = 
        arch.SetComponent(index,&comp)

    
