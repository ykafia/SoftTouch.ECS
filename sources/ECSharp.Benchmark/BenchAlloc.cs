using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using ECSharp.Components;
using System.Linq;
namespace ECSharp.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class BenchAlloc
    {
        TransformComponent[] structs;
        TransformTRSComponent[] classes;

        static int Size = 100;
        
        public BenchAlloc()
        {
            structs = new TransformComponent[Size];
            classes = new TransformTRSComponent[Size].Select( x=>
                new TransformTRSComponent{ 
                    Position = System.Numerics.Vector3.One, 
                    Rotation = System.Numerics.Quaternion.Identity,
                    Scale = System.Numerics.Vector3.One
                    }).ToArray();
        }
        [Benchmark]
        public void StructComponent()
        {
            for(int i = 0; i< structs.Length; i++)
                UpdateStruct(structs[i]);  
        }
        [Benchmark]
        public void StructRefComponent()
        {
            for(int i = 0; i< structs.Length; i++)
                UpdateStruct(ref structs[i]);     
        }
        [Benchmark]
        public void ClassComponent()
        {
            for(int i = 0; i< classes.Length; i++)
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
