using Worms_lab.services;
using Worms_lab.Strategies;

namespace Worms_lab
{
    class Worm
    {
        public string Name { get; init; }
        public int Health { get; private set; }
        public Position Position { get; private set;}

        public WorldSimulator World { get; private set;}

        private readonly IBehaviorStrategy strategy;
        
        public Worm(WorldSimulator world, IBehaviorStrategy strategy, string name, Position pos = new Position())
        {
            Position = pos;
            Health = 10;
            Name = name;
            this.strategy = strategy;
            World = world;
        }

        public (Direction direction, bool split)? GetIntention()
        {
            return strategy.GetIntention(this);
        }

        public void Step(Direction dir)
        {
            Position += dir.GetPosition();
        }

        public void EatFood()
        {
            Health += 10;
        }
        public Worm Split(Direction dir, string name)
        {
            Health -= 10;
            return new Worm(World, strategy, name, Position + dir.GetPosition());
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
