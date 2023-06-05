using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magic_Books_Project.Models;

namespace Magic_Books_Project.Controllers
{

    public class OrdersController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult order_received()
        {
            var bag = (Bag)Session["bag"];
            var new_order_no = db.Orders.Max(x => x.order_id);
            new_order_no = new_order_no + 1;

            foreach (var order_save in bag.Mybag)
            {
                
                Orders order_registration = new Orders
                {
                    order_id = new_order_no,
                    user_id = ((Customers)Session["members"]).customer_id,
                    product_id = order_save.prdct.product_id,
                    order_date = DateTime.Now,
                    quantity = order_save.quantity,
                    total = order_save.prdct.price * order_save.quantity
                    
                };
                db.Orders.Add(order_registration);
                db.SaveChanges();
            }
            Session.Remove("bag");
            return RedirectToAction("my_orders");
        }
        public ActionResult my_orders()
        {
            int customer_id = ((Customers)Session["members"]).customer_id;
            var myorders = db.Orders.Where(x => x.user_id == customer_id);
            var myorders_ = myorders.GroupBy(x => x.order_id);
            return View(myorders_);
        }

    }
}