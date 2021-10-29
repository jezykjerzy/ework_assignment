using EworkAssignment.Models;
using FizzWare.NBuilder;
using FizzWare.NBuilder.PropertyNaming;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EworkAssignment.Data
{

    public abstract class DataFactory
    {
        internal Price[] GeneratePrices(int pricesCount, IProductKeyResolver productKeyResolver)
        {
            return Builder<Price>.CreateListOfSize(pricesCount)
                                 .All()
                                 .With(price => price.Value = Faker.RandomNumber.Next(1, 10000))
                                 .With(price => price.ProductKey = productKeyResolver.Resolve())
                                 .Build()
                                 .ToArray();
        }
    }



    internal class DataFactoryForTask1 : DataFactory
    {
        private readonly int _positionCount;
        private readonly int _pricesCount;
        private IProductKeyResolver _productKeyResolver;

        public DataFactoryForTask1(int positionCount, int pricesCount)
        {
            _productKeyResolver = new ProductKeyResolver(positionCount);
            _positionCount = positionCount;
            _pricesCount = pricesCount;
        }

        public Price[] GeneratePrices()
        {
            return GeneratePrices(_pricesCount, _productKeyResolver);
        }

        internal Position[] GeneratePositions()
        {
            return Builder<Position>.CreateListOfSize(_positionCount)
                                    .All()
                                    .With(position => position.Amount = Faker.RandomNumber.Next(1, 1000))
                                    .With(position => position.ProductKey = _productKeyResolver.Resolve())
                                    .Build()
                                    .OrderBy(position => position.Amount)
                                    .ToArray();
        }

    }

    internal class DataFactoryForTask2 : DataFactory
    {
        private IProductKeyResolver _productKeyResolver;

        public DataFactoryForTask2(int productKeyCount)
        {
            _productKeyResolver = new ProductKeyResolver(productKeyCount);
        }

        public Price[] GeneratePrices(int pricesCount)
        {
            return GeneratePrices(pricesCount, _productKeyResolver);
        }

    }
}