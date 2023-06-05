using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using Magic_Books_Project.Models;
using CrystalDecisions.CrystalReports.Engine;

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
            var revenue = db.Orders.Sum(x => x.total);
            var product = db.Product.Count();
            var top_selling = db.Orders.ToList();
            ViewBag.orders = ordercount;
            ViewBag.customers = customercount;
            ViewBag.revenue = revenue;
            ViewBag.product = product;
            return View();
        }
        public ActionResult Prod()
        {
            return View();
        }
        public ActionResult Product_edit()
        {
            return View();
        }

        public ActionResult customer_list()
        {
            var customer = db.Customers.ToList();
            return View(customer);
        }
        public ActionResult customers_report(byte? id)
        {
            var customers = from member in db.Customers
                            select new
                            {

                                member.first_name,
                                member.last_name,
                                member.username,

                                member.e_mail
                            };
            ReportDocument rapor = new ReportDocument();
            rapor.Load(Server.MapPath("~/crystal_reports/customers_report.rpt"));
            rapor.SetDataSource(customers);
            Response.Buffer = false;
            Response.ClearContent();
            if (id == 1)
            {
                Stream stream = rapor.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "customers_report.pdf");
            }
            else
            {
                Stream stream = rapor.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/xls", "customers_report.xls");
            }

        }
        public ActionResult order_list()
        {
            var orders = db.Orders.GroupBy(x => x.order_id);
            return View(orders);
        }

        
        public ActionResult orders_report(byte? id)
        {
            var orders = from member in db.Customers
                            from order in db.Orders
                            from product in db.Product
                            where order.product_id == product.product_id && order.user_id == member.customer_id
                            select new
                            {
                                member.first_name,
                                member.customer_id,
                                product.name,
                                order.order_date,
                                order.quantity,
                                product.price,
                                order.order_id
                            };
            ReportDocument rapor = new ReportDocument();
            rapor.Load(Server.MapPath("~/crystal_reports/orders_report.rpt"));
            rapor.SetDataSource(orders);
            Response.Buffer = false;
            Response.ClearContent();
            if (id == 1)
            {
                Stream stream = rapor.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "orders_report.pdf");
            }
            else
            {
                Stream stream = rapor.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/xls", "orders_report.xls");
            }



        }


    }
}