using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Manger
{
    public class Cart
    {
        HashSet<CartLine> lineCollection = new HashSet<CartLine>();
        public void AddItem(Product productpram , int quantity) {

            CartLine product = new CartLine() {Product=productpram,Quantity=quantity };
            var line = lineCollection
                .Where(L => L.Product.ProductID == productpram.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(product);
            }
            else { line.Quantity += quantity; }
        } 
        public void Remove (Product productPram)
        {
            List<CartLine> line = lineCollection
               .Where(L => L.Product.ProductID == productPram.ProductID)
               .ToList();
            var myline = lineCollection
               .Where(L => L.Product.ProductID == productPram.ProductID).FirstOrDefault();


            if (line.Count() > 1)
            {
                lineCollection
                 .Where(L => L.Product.ProductID == productPram.ProductID).FirstOrDefault().Quantity--;
            }
            else if (line.Count()==1)
            {
                lineCollection.RemoveWhere(l => l.Product.ProductID == productPram.ProductID);
            }

            
        }
        public decimal ComputeTotalValue() {

            return lineCollection.Sum(l=>l.Product.Price*l.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> lines
        {
            get { return lineCollection; }
        }
    }
}
