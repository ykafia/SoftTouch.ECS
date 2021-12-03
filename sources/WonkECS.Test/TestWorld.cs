using Microsoft.VisualStudio.TestTools.UnitTesting;
using WonkECS;
using WonkECS.Components;

namespace WonkECS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddSameComponent()
        {
            var world = new World();
            world
                .CreateEntity()
                .With(new NameComponent{Name = "Lola"})
                .With(new NameComponent{Name = "Jojo"})
                .Build();
            
            world[0].
        }
    }
}

