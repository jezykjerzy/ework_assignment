using System;

namespace EworkAssignment.Data
{
    // Could be more sophisticated resolver
    internal interface IProductKeyResolver
    {
        string Resolve();
    }

    internal class ProductKeyResolver : IProductKeyResolver
    {
        private int _productKeyCount;
        private int counter;

        public ProductKeyResolver(int productKeyCount)
        {
            _productKeyCount = productKeyCount;
        }

        public string Resolve()
        {
            if(counter >= _productKeyCount)
            {
                counter = 0;
            }

            return $"ProductKey {counter++}";
        }
    }
}