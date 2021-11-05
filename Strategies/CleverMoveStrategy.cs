using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Strategies
{
    class CleverMoveStrategy : IBehaviorStrategy
    {

        public (Direction Direction, bool Split)? GetIntention(Worm worm)
        {
            if (worm.Health > 20)
                return (DirectionExtension.GetRandomDirection(), true);
            Food nearestFood = GetNearestFood(worm);
            if (nearestFood == null)
                return null;
            return (GetDirectionToFood(worm, nearestFood.Position), false);
        } 

        private Food GetNearestFood(Worm worm)
        {

            var foodList = worm.World.GetFood();
            Food nearestFood = null;
            int minDist = int.MaxValue;
            foreach (var food in foodList)
            {
                int dist = food.Position.Distance(worm.Position);
                if (food.Freshness >= dist && worm.Health >= dist && minDist > dist)
                {
                    minDist = dist;
                    nearestFood = food;
                }
            }
            return nearestFood;
        }

        private Direction GetDirectionToFood(Worm worm, Position foodPosition)
        {
            Position wormPosition = worm.Position;
            if (wormPosition.X == foodPosition.X)
            {
                return (wormPosition.Y < foodPosition.Y) ? Direction.UP : Direction.DOWN;
            }
            if (wormPosition.Y == foodPosition.Y)
            {
                return (wormPosition.X < foodPosition.X) ? Direction.RIGHT : Direction.LEFT;
            }
            Direction dir = (wormPosition.X < foodPosition.X) ? Direction.RIGHT : Direction.LEFT;
            if (worm.World.IsWorm(dir.GetPosition() + wormPosition))
            {
                dir = (wormPosition.Y < foodPosition.Y) ? Direction.UP : Direction.DOWN;
            }
            return dir;

        }

    }
}
