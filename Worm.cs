using Worms_lab.services;
using Worms_lab.Strategies;

namespace Worms_lab
{
    class Worm
    {
        public string Name { get; init; }
        public int Health { get; private set; }
        public Position Position { get; private set; }

        private readonly IBehaviorStrategy strategy;
        public (Direction Direction, bool Split)? Intention { get; private set; }

        public Worm(IBehaviorStrategy strategy, Position pos = new Position())
        {
            Position = pos;
            Health = 10;
            Name = NameGenerator.GenerateName();
            this.strategy = strategy;
        }

        public void makeIntention()
        {
            Intention = strategy.GetIntention(this);
        }

        public void Step(Direction dir)
        {
            Position += dir.GetPosition();
        }

        public void EatFood()
        {
            Health += 10;
        }
        public Worm Split(Direction dir)
        {
            Health -= 10;
            return new Worm(strategy, Position + dir.GetPosition());
        }

        public void UpdateHealth()
        {
            Health--;
        }

        public override string ToString()
        {
            return $"{Name}-{Health}{Position}";
        }

    }
}
