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
            Commands.Add("exit", Exit);
        }
        public static void CreateRecord()
        {
            Stock _stock;
            Console.WriteLine("Введите название актива");
            string name = Console.ReadLine();
            Console.WriteLine("Введите URL страницы слежения");
            string url = Console.ReadLine();
            Console.WriteLine("Введите селектор показателя");
            string selector = Console.ReadLine();
            _stock = new Stock(name, new(url, selector));
            _stock.UpData();
            DataBuffer.List.Add(_stock);

            SaveAll();
        }
        public static void GetList()
        {
            foreach (Stock item in DataBuffer.List)
            {
                item.UpData();
                Console.Write($"{item.Id}:  {item.Name}  ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{item.CurrentPrice}");
                Console.ResetColor();

            }
            SaveAll();
        }
        public static void Exit()
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
