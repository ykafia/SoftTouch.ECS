using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECSharp;
using ECSharp.Components;
using ECSharp.Processors;

namespace ECSharp.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestCreateEntity()
        {
            var world = new World();
            world
                .CreateEntity()
                .With(new NameComponent{Name = "Lola"})
                .Build();
            Assert.IsTrue(world[0].Has<NameComponent>());
            Assert.AreEqual(world[0].Get<NameComponent>().Name,  "Lola");
        }

        [TestMethod]
        public void TestAddSameComponent()
        {
            var world = new World();
            world
                .CreateEntity()
                .With(new NameComponent{Name = "Lola"})
                .With(new NameComponent{Name = "Jojo"})
                .Build();
            
            Assert.AreEqual(world[0].Get<NameComponent>().Name,  "Jojo");
        }

        [TestMethod]
        public void TestRemoveComponent()
        {
            var world = new World();
            world
                .CreateEntity()
                .With(new NameComponent{Name = "Lola"})
                .With(new HealthComponent{LifePoints = 100})
                .Build();
            world[0].Remove<NameComponent>();
            Assert.IsFalse(world[0].Has<NameComponent>());
            Assert.IsTrue(world[0].Has<HealthComponent>());
        }
        [TestMethod]
        public void TestAddComponent()
        {
            var world = new World();
            world
                .CreateEntity()
                .With(new HealthComponent{LifePoints = 100})
                .Build();
            world[0].Add(new NameComponent{Name = "Lola"});
            Assert.IsTrue(world[0].Has<NameComponent>());
            Assert.IsTrue(world[0].Has<HealthComponent>());
        }

        [TestMethod]
        public void TestSpawnRemove10000()
        {
            var world = new World();
            for(int i = 0; i < 3; i++)
                world.CreateEntity().With(new NameComponent{Name = "Lola"}).With(new HealthComponent()).Build();
            for(int i = 0; i < 3; i++)
                world[i].Remove<NameComponent>();
        }

        [TestMethod]
        public void TestProcessorName()
        {
            var world = new World();
            world.Add(new NameProcessor());
            world.Add(new ModelProcessor());
            world.CreateEntity()
                .With(new NameComponent{Name = "Name"})
                .Build();
            world.CreateEntity()
                .With(new NameComponent{Name = "Name2"})
                .With(new HealthComponent{})
                .With(new ModelComponent())
                .Build();
            world.Update();
            Assert.AreEqual(world[0].Get<NameComponent>().Name, "Lola2");
            Assert.AreEqual(world[1].Get<NameComponent>().Name, "Lola2");            
        }
    }
}

