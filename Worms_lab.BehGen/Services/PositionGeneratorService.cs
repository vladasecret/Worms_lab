using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.BehGen
{
    public class PositionGeneratorService
    {
        private Random random;
        private double mu;
        private double sigma;

        public PositionGeneratorService(double mu = 0, double sigma = 1)
        {
            this.mu = mu;
            this.sigma = sigma;
            random = new Random();
        }

        public (int x, int y) Generate()
        {
            int x = random.NextNormal(mu, sigma);
            int y = random.NextNormal(mu, sigma);
            return (x, y);
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
