using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Strategies
{
    class CircleMoveStrategy : IBehaviorStrategy
    {
        private readonly Dictionary<Position, Direction> circle;
        
        private Position Center { get; init; }
        
        
        
        public CircleMoveStrategy()
        {
            Center = new Position(0, 0); 
             circle = new Dictionary<Position, Direction>() {
                {new Position(0, 1), Direction.RIGHT } ,
                {new Position(1, 1), Direction.DOWN },
                {new Position(1, 0), Direction.DOWN },
                {new Position(1, -1), Direction.LEFT },
                {new Position(0, -1), Direction.LEFT },
                {new Position(-1, -1), Direction.UP},
                {new Position(-1, 0), Direction.UP },
                {new Position(-1, 1), Direction.RIGHT }
            };
        }

        public (Direction Direction, bool Split)? GetIntention(Worm worm)
        {
            Direction dir;
            if (worm.Position == Center) 
            {
                dir = Direction.UP;
            }
            else if (!circle.TryGetValue(worm.Position - Center, out dir))
                throw new BadPositionException("The worm should be in a circle centered at point (0,0)");
            return (dir, false);
        }
    }

    class BadPositionException : Exception
    {
        public BadPositionException(string msg) : base(msg)
        {

        }
    }
}
