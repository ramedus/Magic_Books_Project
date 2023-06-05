using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magic_Books_Project.Models;
namespace Magic_Books_Project.Controllers
{
    public class BagController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        // GET: Bag
        public ActionResult Index()
        {
            return View(bag());
        }
        public Bag bag()//sepeti getir
        {
            var bag =(Bag) Session["bag"];
            if (bag == null)
            {
                bag = new Bag();
                Session["bag"] = bag;
            }
            return bag;
        }
        public ActionResult add_bag_cntrl(int? product_id,byte? quantity)
        {
            var _quantity = quantity ?? 0;
            var product = db.Product.FirstOrDefault(x => x.product_id == product_id);
            if (product!=null)
            {
                bag().add_bag(product, _quantity);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }
        public ActionResult remove_product(int? product_id)
        {
            var product = db.Product.FirstOrDefault(x => x.product_id== product_id);
            if (product!= null)
            {
                bag().deleted_prdct(product);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }

    }
}