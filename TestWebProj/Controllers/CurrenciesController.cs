using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebProj.Models;

namespace TestWebProj.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly IMemoryCache memoryCache;
        public CurrenciesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public IActionResult Index(int pg = 1)
        {
            if (!memoryCache.TryGetValue("key_currency", out List<CurrencyModel> currencies))
                throw new Exception("Ошибка получения данных");

            const int pageSize = 4;
            if (pg < 1) pg = 1;

            int recsCount = currencies.Count();

            var pager = new PagerModel(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = currencies.Skip(recSkip).Take(pager.PageSize);

            this.ViewBag.Pager = pager;

            //return View(currencies);

            return View(data);
        }
        public IActionResult Currency(string Id)
        {
            if (!memoryCache.TryGetValue("key_currency", out List<CurrencyModel> currencies))
                throw new Exception("Ошибка получения данных");
            CurrencyModel currency = currencies.Single(c => c.CharCode == Id);

            return View(currency);
        }
    }
}
