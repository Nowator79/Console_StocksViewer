using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_recommendation_system.Modules
{
    public class LinkStock
    {
        public string UrlStatistics { get; set; }
        public string Selector { get; set; }

        public LinkStock()
        {
        }

        public LinkStock(string urlStatistics, string selector)
        {
            this.UrlStatistics = urlStatistics;
            this.Selector = selector;
        }
    }
}
