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
using TestWebProj.Models;

namespace TestWebProj.Services
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
                    string json = string.Empty;
                    List<CurrencyModel> currencies = new List<CurrencyModel>();

                    try
                    {
                        json = wc.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js");

                        var document = JsonDocument.Parse(json);
                        var rootElement = document.RootElement;
                        var valute = rootElement.GetProperty("Valute");
                        var map = JsonConvert.DeserializeObject<Dictionary<string, CurrencyModel>>(valute.ToString());

                        foreach (var item in map)
                        {
                            currencies.Add(item.Value);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    memoryCache.Set("key_currencies", currencies, TimeSpan.FromMinutes(120));
                }

                await Task.Delay(3600000);
            }
        }
    }
}
