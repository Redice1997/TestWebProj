using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TestModel
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }        
        public Valute Valute { get; set; }
    }

    public class Valute
    {
        public Currency AUD { get; set; }
        public Currency AZN { get; set; }
        public Currency GBP { get; set; }
        public Currency AMD { get; set; }
        public Currency BYN { get; set; }
        public Currency BGN { get; set; }
        public Currency BRL { get; set; }
        public Currency HUF { get; set; }
        public Currency HKD { get; set; }
        public Currency DKK { get; set; }
        public Currency USD { get; set; }
        public Currency EUR { get; set; }
        public Currency INR { get; set; }
        public Currency KZT { get; set; }
        public Currency CAD { get; set; }
        public Currency KGS { get; set; }
        public Currency CNY { get; set; }
        public Currency MDL { get; set; }
        public Currency NOK { get; set; }
        public Currency PLN { get; set; }
        public Currency RON { get; set; }
        public Currency XDR { get; set; }
        public Currency SGD { get; set; }
        public Currency TJS { get; set; }
        public Currency TRY { get; set; }
        public Currency TMT { get; set; }
        public Currency UZS { get; set; }
        public Currency UAH { get; set; }
        public Currency CZK { get; set; }
        public Currency SEK { get; set; }
        public Currency CHF { get; set; }
        public Currency ZAR { get; set; }
        public Currency KRW { get; set; }
        public Currency JPY { get; set; }
    }

    public class Currency
    {
        public string ID { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Previous { get; set; }
    }

}



