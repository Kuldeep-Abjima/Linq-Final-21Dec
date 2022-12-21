using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.EntityClasses
{
    public  class ProductComparer : EqualityComparer<Product>
    {
        public override bool Equals(Product x, Product y)
        {
            return (x.ProductID == y.ProductID && 
                    x.Name == y.Name &&
                    x.Color == y.Color &&
                    x.Size == y.Size &&
                    x.ListPrice == y.ListPrice &&
                    x.StandardCost == y.StandardCost);
        }
        public override int GetHashCode(Product obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }
}
