using System;
using System.Collections.Generic;
using System.IO;
using Worms_lab.Strategies;

namespace Worms_lab
{
    class Program
    {
        public Position Position { get; private set; }
        static void Main(string[] args)
        {
            StreamWriter streamWiter = new StreamWriter("log.txt");
            new World(streamWiter).Live();   
            streamWiter.Close();

        }
    }




}
