using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS.Shared.Components;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SoftTouch.ECS.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class BenchAlloc
    {
        List<TransformComponent> structs;
        List<TransformTRSComponent> classes;

        static int Size = 1000;

        public BenchAlloc()
        {
            structs = new List<TransformComponent>(Size);
            classes = new List<TransformTRSComponent>(Size).Select(_ =>
                new TransformTRSComponent
                {
                    Position = System.Numerics.Vector3.One,
                    Rotation = System.Numerics.Quaternion.Identity,
                    Scale = System.Numerics.Vector3.One
                }).ToList();
        }
        [Benchmark]
        public void StructComponent()
        {
            for (int i = 0; i < structs.Count; i++)
                UpdateStruct(structs[i]);
        }
        [Benchmark]
        public void StructRefComponent()
        {
            for (int i = 0; i < structs.Count; i++)
                UpdateStruct(ref CollectionsMarshal.AsSpan(structs)[i]);
        }
        [Benchmark]
        public void ClassComponent()
        {
            for (int i = 0; i < classes.Count; i++)
                UpdateClass(classes[i]);
        }

        public static TransformComponent UpdateStruct(TransformComponent v)
        {
            return new TransformComponent
            {
                Position = v.Position + System.Numerics.Vector3.One,
                Rotation = v.Rotation * 2,
                Scale = v.Scale + System.Numerics.Vector3.One
            };
        }
        public static void UpdateStruct(ref TransformComponent v)
        {
            v.Position += System.Numerics.Vector3.One;
            v.Rotation *= 2;
            v.Scale += System.Numerics.Vector3.One;
        }
        public static void UpdateClass(TransformTRSComponent v)
        {
            v.Position += System.Numerics.Vector3.One;
            v.Rotation *= 2;
            v.Scale += System.Numerics.Vector3.One;
        }

    }
}
