using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {           

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js");

                string[] valuteNames = new[]
                {
                    "AUD", "BGN", "USD", "KGS", "RON", "TMT", "CHF",
                    "AZN", "BRL", "EUR", "CNY", "XDR", "UZS", "ZAR",
                    "GBP", "HUF", "INR", "MDL", "SGD", "UAH", "KRW",
                    "AMD", "HKD", "KZT", "NOK", "TJS", "CZK", "JPY",
                    "BYN", "DKK", "CAD", "PLN", "TRY", "SEK",
                };

                List<Currency> currencies = new List<Currency>();

                var document = JsonDocument.Parse(json);
                var rootElement = document.RootElement;
                var valute = rootElement.GetProperty("Valute");

                foreach (var name in valuteNames)
                {
                    var v = valute.GetProperty(name);
                    var s = v.ToString();
                    var currency = JsonConvert.DeserializeObject<Currency>(s);
                    currencies.Add(currency);
                }
                foreach (var item in currencies)
                {
                    string s = $"{item.CharCode} '{item.Name}' Курс: {item.Value} руб.";
                    Console.WriteLine(s);
                }
            }

            
            Console.ReadLine();
        }
    }
}
