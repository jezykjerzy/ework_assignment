using EworkAssignment.Models;
using FizzWare.NBuilder;
using FizzWare.NBuilder.PropertyNaming;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EworkAssignment.Data
{
    internal class DataFactory
    {

        private const int PriceCount = 1000000;
        private const int PositionCount = 1000;
        private ProductKeyResolver _productKeyResolver;

        public DataFactory()
        {
            _productKeyResolver = new ProductKeyResolver(PositionCount);
        }

        internal Position[] GeneratePositions()
        {
            return Builder<Position>.CreateListOfSize(PositionCount)
                                    .All()
                                    .With(position => position.Amount = Faker.RandomNumber.Next(1, 1000))
                                    .With(position => position.ProductKey = _productKeyResolver.Resolve())
                                    .Build()
                                    .OrderBy(position => position.Amount)
                                    .ToArray();
        }
            

        internal Price[] GeneratePrices()
        {
            return Builder<Price>.CreateListOfSize(PriceCount)
                                 .All()
                                 .With(price => price.Value = Faker.RandomNumber.Next(1, 10000))
                                 .With(price => price.ProductKey = _productKeyResolver.Resolve())
                                 .Build()
                                 .ToArray();
        }
    }
}