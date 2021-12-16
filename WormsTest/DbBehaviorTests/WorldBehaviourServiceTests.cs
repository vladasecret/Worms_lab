using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worms_lab.Data;
using Worms_lab.DBAware;

namespace WormsTest.DbBehaviorTests
{
    [TestFixture]
    class WorldBehaviourServiceTests : DbBehaviourServicesTestBase
    {
        public WorldBehaviourServiceTests()
            : base(new DbContextOptionsBuilder<EnvironmentContext>()
                .UseInMemoryDatabase(databaseName: "EnvironmentDatabase")
                .Options)
        {
            
        }

        [SetUp]
        public void SetUp()
        {
            SeedData();
        }

        [Test]
        public void Test_Loading() => TestLoading();
        

        [Test]
        public void BadLoadingTest()
        {
            var worldBehaviourService = new DbAwareWorldBehaviourService(ContextFactory);
            try
            {
                worldBehaviourService.Load("BadName");
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }


        [Test]
        public void BadIndexTest()
        {
            var worldBehaviourService = new DbAwareWorldBehaviourService(ContextFactory);
            var step = -1;
            try
            {
                worldBehaviourService.GetFoodOnStep(step);
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        



    }
}
