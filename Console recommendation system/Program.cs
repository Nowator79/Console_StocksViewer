using ConsoleRecommendationSystem.Modules;
using System;
using System.Collections.Generic;

namespace ConsoleRecommendationSystem
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("___Start___");
            ConsoleScripts.Init();

            while (!DataBuffer.Exit)
            {
                Console.Write(">");
                string command = Console.ReadLine();
                if (ConsoleScripts.Commands.ContainsKey(command))
                {
                    ConsoleScripts.Commands[command]();
                }
                else
                {
                    Console.WriteLine("no command");
                }
            }

            Console.WriteLine("____End____");
        }
    }
}
