using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TeleTraderAssignment
{
    public class Data
    {
        public List<Symbol> symbolList { get; set; }
        static HttpClient httpClient;
        public Data(List<Symbol> symbolList)
        {
            this.symbolList = symbolList;
            httpClient = new HttpClient();
        }

        public async Task<string> getValues()// Get price values and fill data for all symbols.
        {
            var url = "https://api-pub.bitfinex.com/v2/tickers?symbols=";
            for (int i = 0; i < symbolList.Count; i++)
            {
                if (i == 0)
                {
                    url += "t" + symbolList.ElementAt(i).ticker;
                }
                else
                {
                    url += "%2Ct" + symbolList.ElementAt(i).ticker;
                }
            }
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> getValues(List<string> ids)// Get price values and fill data for specific symbols which ids were given as argument.
        {
            ids.Contains(symbolList.ElementAt(0).id);
            List<Symbol> temp = symbolList.Where(s => ids.Contains(s.id)).ToList();
            var url = "https://api-pub.bitfinex.com/v2/tickers?symbols=";
            for (int i = 0; i < temp.Count; i++)
            {
                if (i == 0)
                {
                    url += "t" + temp.ElementAt(i).ticker;
                }
                else
                {
                    url += "%2Ct" + temp.ElementAt(i).ticker;
                }
            }
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        public float fromStringToFloat(string s)//Parse string to float where decimal separator is culture independent.
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }
        public Symbol fillValues(string s)//Fills symbol attributes with data from string.
        {
            string[] temp2 = s.Split(",");
            Symbol tempSymbol = symbolList.Where(s => temp2[0].Contains(s.ticker)).FirstOrDefault();
            tempSymbol.bidPrice = fromStringToFloat(temp2[1]);
            tempSymbol.askPrice = fromStringToFloat(temp2[3]);
            tempSymbol.lastPrice = fromStringToFloat(temp2[7]);
            tempSymbol.highPrice = fromStringToFloat(temp2[9]);
            tempSymbol.lowPrice = fromStringToFloat(temp2[10]);
            return tempSymbol;
        }
    }
}
