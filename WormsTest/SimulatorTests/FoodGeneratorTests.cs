using Moq;
using NUnit.Framework;
using Worms_lab.Simulator.Services;
using Worms_lab.Simulator;

namespace WormsTest.SimulatorTests
{
    class FoodGeneratorTests
    {
        [Test]
        [TestCase(50)]
        public void UniqueNames(int num)
        {
            var generator = new NormalFoodGeneratorService();
            var state = new WorldState();
            var stateRO = state.AsReadOnly();
            for (int i = 0; i < num; ++i)
            {
                state.Food.Add(generator.Generate(stateRO));
            }
            Assert.AreEqual(num, state.Food.Count);
        }
    }
}
