namespace SoftTouch.ECS.FSharp

open SoftTouch.ECS
open SoftTouch.ECS.Arrays
open FSharp.Core
open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open SoftTouch.ECS.Storage
open SoftTouch.ECS.Processors
open SoftTouch.ECS.Querying

module ProcessorTypes =   

    type FProcessor<'Q1 when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)>
        (updater : 'Q1 -> unit) =
        inherit Processor<'Q1>(null)
        override this.Update() = 
            updater this.Query
    
    let createProcessor1<'Q1 when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)>
        (updater : 'Q1 -> unit) = 
        FProcessor<'Q1>(updater)
    
    type FProcessor<'Q1, 'Q2 
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)> 
        (updater : 'Q1 -> 'Q2 -> unit) =
        inherit Processor<'Q1,'Q2>(null)
        override this.Update() = 
            updater this.Query1 this.Query2
    
    let createProcessor2<'Q1, 'Q2 
                    when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                    and 'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)> 
            (updater : 'Q1 -> 'Q2 -> unit) = 
        FProcessor<'Q1,'Q2>(updater)
    
    type FProcessor<'Q1, 'Q2, 'Q3
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)> 
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> unit) =
        inherit Processor<'Q1,'Q2, 'Q3>(null)
        override this.Update() = 
            updater this.Query1 this.Query2 this.Query3
    
    let createProcessor3<'Q1, 'Q2, 'Q3
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)> 
            (updater : 'Q1 -> 'Q2 -> 'Q3 -> unit) = 
        FProcessor<'Q1,'Q2, 'Q3>(updater)
    
    type FProcessor<'Q1, 'Q2, 'Q3, 'Q4
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)
                and 
                   'Q4 :> IWorldQuery and 'Q4 : struct and 'Q4 :> ValueType and 'Q4 : (new: unit -> 'Q4)> 
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4 -> unit) =
        inherit Processor<'Q1,'Q2,'Q3,'Q4>(null)
        override this.Update() = 
            updater this.Query1 this.Query2 this.Query3 this.Query4
    
    let createProcessor4<'Q1, 'Q2, 'Q3, 'Q4
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)
                and 
                   'Q4 :> IWorldQuery and 'Q4 : struct and 'Q4 :> ValueType and 'Q4 : (new: unit -> 'Q4)> 
            (updater : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4 -> unit) = 
        FProcessor<'Q1,'Q2,'Q3,'Q4>(updater)

    


module Commands =
    let spawn (commands : Commands) : EntityCommands =
        commands.Spawn()

    let spawnw (commands : WorldCommands) : EntityCommands =
        commands.SpawnEmpty()

    let spawnr (commands : Resource<WorldCommands>) : EntityCommands =
        commands.Content.SpawnEmpty()

    let With<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T>> 
        (builder : EntityCommands) : EntityCommands=
        builder.Insert<'T>()
    
    let WithValue<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T>> 
        (c : 'T) (builder : EntityCommands) : EntityCommands =
        builder.Insert(&c)

module World =
    let spawn (world : World) =
        world.Commands.SpawnEmpty()

    let getEntity  (index : int32) (world : World) =
        world[index]

module App =
    open SoftTouch.ECS.Scheduling
    let update (frames: int) (app : App) =
        for _ in 0..frames do app.Update()
        app
    let run(app : App) =
        app.Run()
        app

    let addGenericProcessor<'T when 'T :> Processor and 'T : (new: unit -> 'T)>(app : App) =
        app.AddProcessor<'T>()
    let addProcessor (processor : Processor) (app : App) =
        app.AddProcessor(processor)
    let addProcessors (processors: Processor array) (app : App) =
        app.AddProcessors<Update>(processors)
    let addProcessorsTo<'t & #SubStage> (processors: Processor list) (app : App) =
        app.AddProcessors<'t>(processors |> List.toArray)

    let addStartupProcessor<'T when 'T :> Processor and 'T : (new: unit -> 'T)>(processor, app : App) =
        app.AddStartupProcessor<'T>()



module QueryEntity =
    
    let inline Get<
                'T, 'Q1 when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T> and 'Q1 :> IWorldQuery 
                and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1) and 'Q1 :> IEntityQuery >
        (e : QueryEntity<'Q1>) =
        e.Get<'T>()
    let inline  SetGet<
                'T, 'Q1 when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T> and 'Q1 :> IWorldQuery 
                and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1) and 'Q1 :> IEntityQuery >
                (cmp : 'T) (e : QueryEntity<'Q1>) =
        e.Set<'T>(&cmp)
    

module Entity =
    
    let Get<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T>> (e : Entity) =
        raise(NotImplementedException())
        //e.Get<'T>()
    let Set<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T>> (cmp : 'T) (e : Entity) =
        raise(NotImplementedException())
        //e.Set<'T>(&cmp)
    let Has<'T when 'T : struct and 'T : (new: unit -> 'T) and 'T :> System.ValueType and 'T :> IEquatable<'T>> (e : Entity) =
        raise(NotImplementedException())
        //e.Get<'T>()

module Processor =

    open ProcessorTypes

    let Add<'Q1 when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)>
        (updater : 'Q1 -> unit) (app : App) : App =
        app.AddProcessor((createProcessor1 updater))
    
    let Add2<'Q1, 'Q2 
            when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
            and 'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)> 
        (updater : 'Q1 -> 'Q2 -> unit) (app : App) =
        app.AddProcessor(createProcessor2 updater)

    let Add3<'Q1, 'Q2, 'Q3
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)>
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> unit) (app : App) =
        app.AddProcessor(createProcessor3 updater)
    let Add4<'Q1, 'Q2, 'Q3, 'Q4
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)
                and 
                'Q4 :> IWorldQuery and 'Q4 : struct and 'Q4 :> ValueType and 'Q4 : (new: unit -> 'Q4)>
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4-> unit) (app : App) =
        app.AddProcessor(createProcessor4 updater)


    let AddStartup<'Q1 when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)>
        (updater : 'Q1 -> unit) (app : App) : App =
        app.AddStartupProcessor((createProcessor1 updater))
    
    let AddStartup2<'Q1, 'Q2 
            when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
            and 'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)> 
        (updater : 'Q1 -> 'Q2 -> unit) (app : App) =
        app.AddStartupProcessor(createProcessor2 updater)

    let AddStartup3<'Q1, 'Q2, 'Q3
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)>
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> unit) (app : App) =
        app.AddStartupProcessor(createProcessor3 updater)
    let AddStartup4<'Q1, 'Q2, 'Q3, 'Q4
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)
                and 
                'Q4 :> IWorldQuery and 'Q4 : struct and 'Q4 :> ValueType and 'Q4 : (new: unit -> 'Q4)>
        (updater : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4-> unit) (app : App) =
        app.AddStartupProcessor(createProcessor4 updater)



module Proc =
    let from<'Q1 when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)> 
            (func : 'Q1 -> unit) (state : StateTransition option)=
        let p = Processor.From<'Q1>(func);
        match state with
        | Some x -> p.When(x)
        | None -> p
    let from2<'Q1, 'Q2 
                    when 'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                    and 'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)> 
            (func : 'Q1 -> 'Q2 -> unit) (state : StateTransition option)=
        let p = Processor.From<'Q1, 'Q2>(func)
        match state with
        | Some x -> p.When(x)
        | None -> p
    
    let from3<'Q1, 'Q2, 'Q3
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)> 
        (func : 'Q1 -> 'Q2 -> 'Q3 -> unit) (state : StateTransition option) =
        let p = Processor.From<'Q1, 'Q2, 'Q3>(func)
        match state with
        | Some x -> p.When(x)
        | None -> p
    
    let from4<'Q1, 'Q2, 'Q3, 'Q4
                when 
                   'Q1 :> IWorldQuery and 'Q1 : struct and 'Q1 :> ValueType and 'Q1 : (new: unit -> 'Q1)
                and 
                   'Q2 :> IWorldQuery and 'Q2 : struct and 'Q2 :> ValueType and 'Q2 : (new: unit -> 'Q2)
                and 
                   'Q3 :> IWorldQuery and 'Q3 : struct and 'Q3 :> ValueType and 'Q3 : (new: unit -> 'Q3)
                and 
                   'Q4 :> IWorldQuery and 'Q4 : struct and 'Q4 :> ValueType and 'Q4 : (new: unit -> 'Q4)> 
        (func : 'Q1 -> 'Q2 -> 'Q3 -> 'Q4 -> unit) (state : StateTransition option) =
        let p = Processor.From<'Q1, 'Q2, 'Q3, 'Q4>(func)
        match state with
        | Some x -> p.When(x)
        | None -> p
