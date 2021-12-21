using ConsoleRecommendationSystem.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRecommendationSystem
{
    public static class ConsoleScripts
    {
        public delegate void Commend();
        private static Dictionary<string, Commend> commends = new();
        public static Dictionary<string, Commend> Commands { get => commends; set => commends = value; }


        public static void Init()
        {
            LoadAll();

            Commands.Add("create", CreateRecord);
            Commands.Add("getlist", GetList);
            Commands.Add("obs", Observation);
            Commands.Add("exit", Exit);
        }
        private static void Observation()
        {
            Console.Clear();

            while (true)
            {

                foreach (Stock item in DataBuffer.List)
                {
                    item.UpData();
                    item.GetShortInfoConsole();
                }
                Console.SetCursorPosition(0, 0);


            }
        }
        private static void Updata()
        {
            List<Task> tasks = new();
            foreach (Stock item in DataBuffer.List)
            {
                Task updata = new(() => item.UpData());
                updata.Start();
                tasks.Add(updata);

            }
            foreach (Task item in tasks)
            {
                item.Wait();
            }
        }
        private static void CreateRecord()
        {
            Stock _stock;
            Console.Write("Введите название актива: ");
            string name = Console.ReadLine();
            Console.Write("Введите URL страницы слежения: ");
            string url = Console.ReadLine();
            Console.Write("Введите селектор показателя: ");
            string selector = Console.ReadLine();
            _stock = new Stock(name, new(url, selector));
            _stock.UpData();
            DataBuffer.List.Add(_stock);

            SaveAll();
        }
        private static void GetList()
        {
            Updata();
            foreach (Stock item in DataBuffer.List)
            {
                item.GetInfoConsole();
            }
            SaveAll();
        }
        private static void Exit()
        {
            SaveAll();
            DataBuffer.Exit = true;
        }
        private static void SaveAll()
        {
            foreach (Stock item in DataBuffer.List)
            {
                Stock.SaveStack(item);
            }
        }
        private static void LoadAll()
        {
            DataBuffer.List = Stock.GetALLStocks();
        }
    }
}
