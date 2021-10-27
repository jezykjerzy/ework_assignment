using EworkAssignment.Data;
using EworkAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EworkAssignment.Tasks
{

    class Task1
    {
        private DataFactory _factory;

        public Task1()
        {
            _factory = new DataFactory();
        }
        public void Run()
        {
            
            Price[] prices = GetPrices(); // can be more than 100000 records
            Position[] positions = GetPositions(); // can be more than 1000 records

            MarketValuesCalculator.Calculate(prices, positions);
        }

        private Position[] GetPositions()
        {
            return _factory.GeneratePositions();
        }

        private Price[] GetPrices()
        {
            return _factory.GeneratePrices();
        }

       
    }

    internal class MarketValuesCalculator
    {
        public static void Calculate(Price[] prices, Position[] positions)
        {
            var productKeysToPrices = GetProductKeysToPrices(prices);
            var productKeysToPositions = GetProductKeysToPositions(positions);

            foreach (var productKeyToPosition in productKeysToPositions)
            {
                var productKey = productKeyToPosition.Key;
                var price = GetNewestProductPrice(productKeysToPrices[productKey]);
                var amount = GetNewestProductAmount(productKeyToPosition.Value);
                var message = $"Market value for key: {productKey} is {price * (double)amount}";

                Console.WriteLine(message);
            }
        }

        private static Dictionary<string, List<Position>> GetProductKeysToPositions(Position[] positions)
        {
            return positions.GroupBy(entry => entry.ProductKey)
                            .ToDictionary(entry => entry.Key, entry => entry.ToList());
        }

        private static Dictionary<string, List<Price>> GetProductKeysToPrices(Price[] prices)
        {
            return prices.GroupBy(entry => entry.ProductKey)
                         .ToDictionary(entry => entry.Key, entry => entry.ToList());
        }

        // todo add handling no positions
        private static decimal GetNewestProductAmount(IList<Position> positions)
        {
            return positions.OrderByDescending(position => position.Date)
                            .First()
                            .Amount;
        }

        // todo add handing no prices
        private static double GetNewestProductPrice(IList<Price> prices)
        {
            return prices.OrderByDescending(position => position.Date)
                          .First()
                          .Value;
        }
    }
}