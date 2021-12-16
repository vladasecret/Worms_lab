using Moq;
using NUnit.Framework;
using Worms_lab.Simulator;
using Worms_lab.Simulator.Strategies;


namespace WormsTest.SimulatorTests
{
    class CleverMoveStrategyTests
    {
        [Test]
        public void MoveNearestFood()
        {
            var state = new WorldState();
            var worm = new Worm(state.AsReadOnly(), new CleverMoveStrategy(), "Bob");
            state.Worms.Add(worm);
            state.Food.Add(new Food(new Position(-3, 0)));
            state.Food.Add(new Food(new Position(5, 0)));

            Assert.AreEqual((Direction.LEFT, false), worm.GetIntention());
        }

        [Test]
        public void MovePossibleFood()
        {
            var state = new WorldState();
            var worm = new Worm(state.AsReadOnly(), new CleverMoveStrategy(), "Bob");
            state.Worms.Add(worm);
            state.Food.Add(new Food(new Position(-6, 0), 5));
            state.Food.Add(new Food(new Position(9, 0)));

            Assert.AreEqual((Direction.RIGHT, false), worm.GetIntention());
        }

        [Test]
        public void SplitIfPossible()
        {
            var state = new WorldState();
            var worm = new Worm(state.AsReadOnly(), new CleverMoveStrategy(), "Bob");
            worm.EatFood();
            worm.EatFood(); //Health > 20
            state.Worms.Add(worm);
            var intention = worm.GetIntention();
            Assert.IsTrue(intention.HasValue);
            Assert.IsTrue(intention.Value.split);
        }

        [Test]
        public void NoFoodNoAction()
        {
            var state = new WorldState();
            var worm = new Worm(state.AsReadOnly(), new CleverMoveStrategy(), "Bob");
            state.Worms.Add(worm);
            state.Food.Add(new Food(new Position(-6, 0), 5));
            state.Food.Add(new Food(new Position(3, 2), 4));
            var intention = worm.GetIntention();
            Assert.IsFalse(intention.HasValue);
            
        }
    }
}
