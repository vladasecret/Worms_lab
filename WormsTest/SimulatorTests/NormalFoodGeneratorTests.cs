using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Worms_lab.Simulator;
using Worms_lab.Simulator.Services;

namespace WormsTest.SimulatorTests
{
    class NormalFoodGeneratorTests
    {
        [Test]
        [TestCase(50)]
        public void UniquePositions(int num)
        {
            WorldState state = new();
            var roState = state.AsReadOnly();
            HashSet<Position> set = new();
            var generator = new NormalFoodGeneratorService();
            for (int i = 0; i < num; ++i)
            {
                Food tmp = generator.Generate(roState);
                set.Add(tmp.Position);
                state.Food.Add(tmp);
            }

            Assert.AreEqual(num, set.Count);
                        
        }
    }
}
