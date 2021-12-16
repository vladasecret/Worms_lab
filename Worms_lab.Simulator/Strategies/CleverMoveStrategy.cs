using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator.Strategies
{
    public class CleverMoveStrategy : IBehaviorStrategy
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

            Food nearestFood = null;
            int minDist = int.MaxValue;
            foreach (var foodItem in worm.WorldState.Food)
            {
                int dist = foodItem.Position.Distance(worm.Position);
                if (foodItem.Freshness >= dist && worm.Health >= dist && minDist > dist)
                {
                    minDist = dist;
                    nearestFood = foodItem;
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
            if (worm.WorldState.IsWorm(dir.GetPosition() + wormPosition))
            {
                dir = (wormPosition.Y < foodPosition.Y) ? Direction.UP : Direction.DOWN;
            }
            return dir;

        }

    }
}
