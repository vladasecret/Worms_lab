using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Worms_lab;
using Worms_lab.Services;

namespace WormsTest
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
            var generator = new NormalFoodGenerator();
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
