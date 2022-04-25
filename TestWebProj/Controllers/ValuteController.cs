using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebProj.Models;

namespace TestWebProj.Controllers
{
    public class ValuteController : Controller
    {
        private readonly IMemoryCache memoryCache;
        public ValuteController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            if (!memoryCache.TryGetValue("key_currency", out List<CurrencyModel> currencies))
                throw new Exception("Ошибка получения данных");

            return View(currencies);
        }
        public IActionResult Details(string Id)
        {
            if (!memoryCache.TryGetValue("key_currency", out List<CurrencyModel> currencies))
                throw new Exception("Ошибка получения данных");
            CurrencyModel currency = currencies.Single(c => c.CharCode == Id);

            return View(currency);
        }
    }
}
