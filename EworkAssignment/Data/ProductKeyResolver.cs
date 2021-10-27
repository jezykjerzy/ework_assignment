using System;

namespace EworkAssignment.Data
{
    internal class ProductKeyResolver
    {
        private int _positionCount;
        private int counter;

        public ProductKeyResolver(int positionCount)
        {
            _positionCount = positionCount;
        }

        internal string Resolve()
        {
            if(counter >= _positionCount)
            {
                counter = 0;
            }

            return $"ProductKey {counter++}";
        }
    }
}