using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_lab.Simulator.Services
{
    public class WorldStateWriterService : IDisposable
    {
        private readonly TextWriter streamWriter;

        public WorldStateWriterService(TextWriter writer)
        {
            streamWriter = writer;
        }
        public WorldStateWriterService(string outputFileName) 
        {
            if (string.IsNullOrEmpty(outputFileName))
                outputFileName = "log.txt";
            streamWriter = new StreamWriter(outputFileName);
            //streamWriter = Console.Out;
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
