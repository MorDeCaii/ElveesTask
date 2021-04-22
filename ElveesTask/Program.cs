using System;
using System.IO;
using ElveesTaskClassLibrary;

namespace ElveesTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "Input.txt";
            string outputPath = Directory.GetCurrentDirectory() + "\\Output.txt";

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input file \"" + inputPath + "\" not found");


            string[] lines = File.ReadAllLines("Input.txt");
            if (lines.Length == 0)
                throw new Exception("Input file is empty");


            string[] input = lines[0].Split(' ');
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);

            var graph = new MetroGraph<int>(n, m);
            if (n == 1) graph.AddNode(1);

            for (int i = 1; i < lines.Length; i++)
            {
                input = lines[i].Split(' ');
                int a = int.Parse(input[0]);
                int b = int.Parse(input[1]);
                graph.AddConnection(a, b);
            }

            var sequence = graph.GetCloseSequence();

            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                foreach (var item in sequence)
                {
                    sw.WriteLine(item);
                }
            }
        }
    }
}
