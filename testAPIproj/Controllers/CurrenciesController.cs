using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testAPIproj.Models;

namespace testAPIproj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : Controller
    {
        private readonly IMemoryCache memoryCache;
        public CurrenciesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        [HttpGet]        
        public IEnumerable<CurrencyModel> Index(int pg = 1)
        {
            if (!memoryCache.TryGetValue("key_currencies", out List<CurrencyModel> currencies))
                throw new Exception("Ошибка получения данных");

            const int pageSize = 4;
            if (pg < 1) pg = 1;

            int recsCount = currencies.Count();
            var pager = new PagerModel(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = currencies.Skip(recSkip).Take(pager.PageSize);

            return data;
        }
        [HttpGet("currency/{id}")]
        public decimal Currency(string Id)
        {
            if (!memoryCache.TryGetValue("key_currencies", out List<CurrencyModel> currencies))
                throw new Exception("Ошибка получения данных");
            CurrencyModel currency = currencies.SingleOrDefault(c => c.CharCode.ToUpper() == Id.ToUpper());

            return currency == null ? 0m : (currency.Value / currency.Nominal);
        }
    }
}
