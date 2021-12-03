using Microsoft.VisualStudio.TestTools.UnitTesting;
using WonkECS;
using WonkECS.Components;

namespace WonkECS.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAddComponent()
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
            Assert.IsTrue(world[0].Has<NameComponent>());
        }
    }
}

