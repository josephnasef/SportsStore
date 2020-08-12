using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : Repository<Product>
    {
        public IEnumerable<string> categories()
        {
            return GetAll().Select(c => c.Category).Distinct().OrderBy(c => c);
        }
        public void UpdateProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                Add(product);
            }
            else
            {
               
             Update(product);
            }
        }
        



    }
}
