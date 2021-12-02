using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Worms_lab.Services;

namespace WormsTest
{
    class NameGeneratorTests
    {
        [Test]
        [TestCase(15000)]
        public void UniqueNames(int num)
        {
            var generator = new NameGenerator();
            HashSet<string> names = new();
            for (int i = 0; i < num; ++i)
            {
                names.Add(generator.GenerateName());
            }
            Assert.AreEqual(num, names.Count);
        }
    }
}
