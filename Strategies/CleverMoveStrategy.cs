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
        private readonly World world;

        public CleverMoveStrategy(World world)
        {
            this.world = world;
        }

        public (Direction Direction, bool Split)? GetIntention(Worm worm)
        {
            if (worm.Health > 20)
                return (DirectionExtension.GetRandomDirection(), true);
            Food nearestFood = GetNearestFood(worm);
            if (nearestFood == null)
                return null;
            return (GetDirectionToFood(worm.Position, nearestFood.Position), false);
        }

        private Food GetNearestFood(Worm worm)
        {

            var foodList = world.GetFood();
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

        private int FoodAmountNearby(int distance, Position wormPos)
        {
            var foodList = world.GetFood();
            int res = 0;
            foreach (var food in foodList)
            {
                int dist = food.Position.Distance(wormPos);
                if (dist <= distance && dist >= food.Freshness)
                    res++;
            }
            return res;
        }

        private Direction GetDirectionToFood(Position wormPosition, Position foodPosition)
        {
            if (wormPosition.X == foodPosition.X)
            {
                return (wormPosition.Y < foodPosition.Y) ? Direction.UP : Direction.DOWN;
            }
            if (wormPosition.Y == foodPosition.Y)
            {
                return (wormPosition.X < foodPosition.X) ? Direction.RIGHT : Direction.LEFT;
            }
            Direction dir = (wormPosition.X < foodPosition.X) ? Direction.RIGHT : Direction.LEFT;
            if (world.IsWorm(dir.GetPosition() + wormPosition))
            {
                dir = (wormPosition.Y < foodPosition.Y) ? Direction.UP : Direction.DOWN;
            }
            return dir;

        }

    }
}
