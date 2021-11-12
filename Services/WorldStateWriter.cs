using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab
{
    public class WorldStateWriter : IDisposable
    {
        private readonly StreamWriter streamWriter;

        public WorldStateWriter(string outputFileName) 
        {
            if (String.IsNullOrEmpty(outputFileName))
                outputFileName = "log.txt";
            streamWriter = new StreamWriter(outputFileName);
        }

        public void Dispose()
        {
            streamWriter.Dispose();
        }

        public void Log(WorldState state)
        {
            
            streamWriter.Write($"Worms: [{string.Join(", ", state.Worms)}] ");
            if (state.Food != null || state.Food.Count != 0)
            {
                streamWriter.Write($"Food: [{string.Join(", ", state.Food)}]");
            }
            streamWriter.WriteLine();
        }
        




    }
}
