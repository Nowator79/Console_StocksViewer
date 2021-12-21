using Console_recommendation_system.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_recommendation_system
{
    public static class DataBuffer
    {
        private static List<Stock> list = new();
        public static List<Stock> List { get => list; set => list = value; }



        private static bool exit = false;
        public static bool Exit { get => exit; set => exit = value; }

    }
}
