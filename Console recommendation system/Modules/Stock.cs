using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;
using System.Xml.Linq;
using AngleSharp.Html.Parser;
using System.Threading;

namespace ConsoleRecommendationSystem.Modules
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float AverageBuyPrice { get; set; }
        public float CurrentPrice { get; set; }

        public LinkStock LinkStock { get; set; }


        public Stock(string name, LinkStock linkStock)
        {
            Name = name;
            this.LinkStock = linkStock;
        }

        public Stock()
        {
            Id = 0;
            Name = "empty";
            AverageBuyPrice = 0;
            CurrentPrice = 0;
            LinkStock = new LinkStock();
        }

        public void UpData()
        {
            string _html = string.Empty;
            {

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(LinkStock.UrlStatistics);

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new(objStream);

                string sLine = string.Empty;
                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        _html += sLine;
                }
            }
            HtmlParser parser = new();
            var doc = parser.ParseDocument(_html);
            string _str = doc.QuerySelector(LinkStock.Selector).TextContent;
            float val = (float) Convert.ToDouble(_str);
            CurrentPrice = val;
        }
        public static List<Stock> InitStock()
        {
            List<Stock> stocks = new();


            return stocks;
        }



        public static void SaveStack(Stock _stock)
        {
            string SavePath = Config.stockModulesPath + $@"\{_stock.Name}.xml";
            XmlSerializer formatter = new(typeof(Stock));
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
            using FileStream fs = new(SavePath, FileMode.OpenOrCreate);
            formatter.Serialize(fs, _stock);
        }
        public static Stock LoadStack(string _stackPath)
        {
            XmlSerializer formatter = new(typeof(Stock));
            Stock _stock;
            using (FileStream fs = new(_stackPath, FileMode.OpenOrCreate))
            {
                _stock = (Stock)formatter.Deserialize(fs);
            }
            return _stock;
        }


        public static List<Stock> GetALLStocks()
        {
            List<Stock> _listStock = new();

            string[] allfiles = Directory.GetFiles(Config.stockModulesPath);
            foreach (string filename in allfiles)
            {
                Console.WriteLine($"Loading [{filename}]");
                _listStock.Add(LoadStack(filename));
            }

            return _listStock;

        }
        public void GetInfoConsole()
        {
            Console.Write($"{Id}:  {Name}  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{CurrentPrice}");
            Console.ResetColor();
        }
        public void GetShortInfoConsole()
        {
            Console.Write($"{Name}:  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{CurrentPrice}  ");
            Console.ResetColor();
        }
    }
}
