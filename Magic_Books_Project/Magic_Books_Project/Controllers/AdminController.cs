using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using Magic_Books_Project.Models;


namespace Magic_Books_Project.Controllers
{
    public class AdminController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var customercount = db.Customers.Count();
            var ordercount = db.Orders.Count();
            var revenue = db.Orders.Sum(x => x.)
            ViewBag.orders = ordercount;
            ViewBag.customers = customercount;
            return View();
        }
        public ActionResult customer_report()
        {
            var customers = from customer in db.Customers
                            select new
                            {
                                customer.first_name,
                                customer.last_name,
                                customer.username,
                                customer.e_mail

                            };
            

            return View();
        }
    }
}