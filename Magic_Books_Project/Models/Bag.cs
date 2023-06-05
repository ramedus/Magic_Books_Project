using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magic_Books_Project.Models
{
    public class Bag
    {
        private List<Asset> _my_bag = new List<Asset>();

        public List<Asset> Mybag { get => _my_bag; }

        public void add_bag(Product prdct, byte quantity)
        {
            var existing_product = _my_bag.FirstOrDefault(x => x.prdct.product_id == prdct.product_id);
            if (existing_product == null)//yok ise 1 olarak getirir
            {
                _my_bag.Add(new Asset { prdct = prdct, quantity = 1 });
            }
            else if (quantity == 0) existing_product.quantity += 1;//link ile gelen adet
            else existing_product.quantity = quantity;//sepetteki quantity butonu
                
        }
        public void deleted_prdct(Product deleted_product)
        {
            _my_bag.RemoveAll(x => x.prdct.product_id == deleted_product.product_id);
        }
        public double bag_total()
        {
            return _my_bag.Sum(x => x.prdct.price * x.quantity);
        }
        public void bag_remove()
        {
            _my_bag.Clear();
        }
    }
}