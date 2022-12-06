using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftTouch.ECS;
using SoftTouch.ECS.Components;
using SoftTouch.ECS.Processors;
using System.Linq;

namespace SoftTouch.ECS.Test
{
    [TestClass]
    public class UnitTestReadOnly
    {
        World w;
        World wro;
        World wq;
        World wqro;
        public UnitTestReadOnly()
        {
            w = new();
            wro = new();
            wq = new();
            wqro = new();
            for (int i = 0; i < 100; i++)
            {
                w.CreateEntity().With(new ROHealthComponent(i*10,i*11));
                wro.CreateEntity().With(new ROHealthComponent(i*10,i*11));
                wq.CreateEntity().With(new ROHealthComponent(i*10,i*11));
                wqro.CreateEntity().With(new ROHealthComponent(i*10,i*11));
            }
            w.Add<HealthProcessorE>();
            wro.Add<HealthProcessorRO>();
            wq.Add<HealthProcessorQ>();
            wqro.Add<HealthProcessorQRO>();

        }

        [TestMethod]
        public void TestEnumerable()
        {
            w.Run(100);
        }

        [TestMethod]
        public void TestArchetype()
        {
            wq.Run(100);
        }
        
        [TestMethod]
        public void TestEnumerableRo()
        {
            wro.Run(100);
        }
        
        [TestMethod]
        public void TestArchetypeRo()
        {
            wqro.Run(100);
        }
        
    }
}

