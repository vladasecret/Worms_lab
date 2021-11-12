using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab
{
    public enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    public static class DirectionExtension
    {
        public static Direction GetRandomDirection()
        {
            Array enumArr = Enum.GetValues(typeof(Direction));
            return (Direction)enumArr.GetValue(new Random().Next(enumArr.Length));
        }
        public static Position GetPosition(this Direction dir)
        {

            return dir switch
            {
                Direction.UP => new Position(0, 1),
                Direction.RIGHT => new Position(1, 0),
                Direction.DOWN => new Position(0, -1),
                Direction.LEFT => new Position(-1, 0),
                _ => new Position(0, 0),
            };
        }


    }
}
