using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab
{
    class WorldStateWriter : IDisposable
    {
        private readonly StreamWriter streamWriter;

        public WorldStateWriter(string outputFileName) 
        {
            streamWriter = new StreamWriter(outputFileName);
        }

        public void Dispose()
        {
            streamWriter.Close();
        }

        public void Log(List<Worm> worms, List<Food> food)
        {
            
            streamWriter.Write($"Worms: [{string.Join(", ", worms)}] ");
            if (food != null || food.Count != 0)
            {
                streamWriter.Write($"Food: [{string.Join(", ", food)}]");
            }
            streamWriter.WriteLine();
        }
        




    }
}
