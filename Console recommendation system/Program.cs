using Console_recommendation_system.Modules;
using System;
using System.Collections.Generic;

namespace Console_recommendation_system
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
