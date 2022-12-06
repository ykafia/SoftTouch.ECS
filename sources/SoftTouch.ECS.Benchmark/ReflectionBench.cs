using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS.Components;
using System.Linq;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Arrays;
using System.Text.Json;
using System.Reflection;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SoftTouch.ECS.Benchmark;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(HealthComponent))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}


[MemoryDiagnoser]
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
public class ReflectionBench
{

    public HealthComponent comps1 = new();
    public HealthComponent comps2 = new();
    public HealthComponent aot = new();
    MethodInfo Setter = typeof(HealthComponent).GetProperty("LifePoints").GetSetMethod();

    public HealthComponent comps3 = new();


    static int Size = 10;

    public ReflectionBench()
    {
    }
    [Benchmark]
    public void JSerializer()
    {
        var doc = JsonSerializer.SerializeToNode(comps1);
        doc["LifePoints"] = 50;
        comps1 = JsonSerializer.Deserialize<HealthComponent>(doc);
    }
    [Benchmark]
    public void JsSerializer()
    {
        var doc = JsonSerializer.SerializeToNode(comps1,SourceGenerationContext.Default.HealthComponent);
        doc["LifePoints"] = 50;
        comps1 = JsonSerializer.Deserialize(doc, SourceGenerationContext.Default.HealthComponent);
    }
    [Benchmark]
    public void AOTReflection()
    {
        var prop = comps1.GetProperties().Values.First(x => x.Name == "LifePoints");
        prop.TrySetValue(aot, 50);
        prop.TryGetValue(aot, out var x);
    }
    [Benchmark]
    public void Reflection()
    {
        var set = comps2.GetType().GetProperty("LifePoints").GetSetMethod();
        set.Invoke(comps2, new object[]{51});
    }
    [Benchmark]
    public void CachedReflection()
    {
        Setter.Invoke(comps3,new object[]{52});
    }

}