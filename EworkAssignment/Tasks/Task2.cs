using EworkAssignment.Data;
using EworkAssignment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EworkAssignment.Tasks
{

    class Task2
    {
        private DataFactoryForTask2 _factory;

        public Task2()
        {
            _factory = new DataFactoryForTask2(3);
        }
        public async Task<Price[]> GetPricesFromWebService1()
        {
            Task.Delay(1000).Wait();
             Console.WriteLine("Prices from web service 1 send");
            // Simulate same date and product key
            var prices = _factory.GeneratePrices(1);
            return prices.Concat(prices).ToArray();
        }
        public async Task<Price[]> GetPricesFromWebService2()
        {
            Task.Delay(100).Wait();
            Console.WriteLine("Prices from web service 2 send");
            return _factory.GeneratePrices(2);

        }

        public async Task<Price[]> GetPricesFromWebService3()
        {
            Task.Delay(2000).Wait();
            Console.WriteLine("Prices from web service 3 send");
            return _factory.GeneratePrices(3);
        }



        internal async void Run()
        {
            var userTask = SimulateUserActions();

            var pricesTask = GetPrices();

            // user clicks button to get prices

            var prices = await pricesTask;
            Console.WriteLine(prices.Count());
        }

        private async Task<Price[]> GetPrices()
        {
            Console.WriteLine("User selected Get prices");

            //might be converted to list of tasks
            var webService1Task = GetPricesFromWebService1();
            var webService2Task = GetPricesFromWebService2();
            var webService3Task = GetPricesFromWebService3();

            await Task.WhenAll(webService1Task, webService2Task, webService3Task);


            return MergePrices(webService1Task.Result, webService2Task.Result, webService3Task.Result);
        }

        private Price[] MergePrices(Price[] pricesFromWebService1, Price[] pricesFromWebSevice2, Price[] pricesFromWebService3)
        {
            var prices = new HashSet<Price>(new PricesComparer());
            return prices.Union(pricesFromWebService1)
                        .Union(pricesFromWebSevice2)
                        .Union(pricesFromWebService3)
                        .ToArray();
        }

        public static async Task SimulateUserActions()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(" User action...");
                    Task.Delay(100).Wait();
                }
            });
        }
    }

    internal class PricesComparer : IEqualityComparer<Price>
    {
        public bool Equals(Price x, Price y)
        {
            return x.Date.Equals(y.Date)
                && x.ProductKey.Equals(y.ProductKey);
        }

        public int GetHashCode([DisallowNull] Price price)
        {
            return HashCode.Combine(price.ProductKey, price.Date);
        }
    }
}
