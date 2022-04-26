using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using testAPIproj.Models;

namespace testAPIproj.Services
{
    public class DataService : BackgroundService
    {
        private readonly IMemoryCache memoryCache;
        public DataService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
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

                    List<CurrencyModel> currencies = new List<CurrencyModel>();

                    var document = JsonDocument.Parse(json);
                    var rootElement = document.RootElement;
                    var valute = rootElement.GetProperty("Valute");

                    foreach (var name in valuteNames)
                    {
                        try
                        {
                            var v = valute.GetProperty(name);
                            string s = v.ToString();
                            var currency = JsonConvert.DeserializeObject<CurrencyModel>(s);
                            currencies.Add(currency);
                        }
                        catch (Exception)
                        {

                        }
                    }

                    memoryCache.Set("key_currencies", currencies, TimeSpan.FromMinutes(120));
                }

                await Task.Delay(3600000);
            }
        }
    }
}
