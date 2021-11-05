using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.services
{
    class FoodGenerator
    {
        private readonly Random random;

        public FoodGenerator()
        {
            random = new Random();
        }

        public Food Generate(WorldSimulator world)
        {
            Position position;
            do
            {
                position = new Position(random.NextNormal(0, 5));
            } while (world.IsFood(position));
            return new Food(position);
        }
    }
    static class RandomExtension
    {
        public static int NextNormal(this Random r, double mu = 0, double sigma = 1)

        {
            var u1 = r.NextDouble();

            var u2 = r.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);

        }
    }
}
