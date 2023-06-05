using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Magic_Books_Project.Models;
using System.IO;
using PagedList;

namespace Magic_Books_Project.Controllers
{
    public class HomeController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Categories()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult Login()
        {
            
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(Customers members)
        {
            string mesagge = "";
            var member = db.Customers.FirstOrDefault(x => x.username == members.username && x.password == members.password);
            if (member != null)
            {
                Session["members"] = member;
            }
            else
            {
                mesagge = "Wrong username or password";
            }
            return RedirectToAction("index");
        }
        public ActionResult Fiction()
        {
            
            var fiction = db.Subcategory.Where(x => x.category_id == 1).ToList();

            return PartialView(fiction);
        }
        public ActionResult Nonfiction()
        {

            var nonfiction = db.Subcategory.Where(x => x.category_id == 2).ToList();

            return PartialView(nonfiction);
        }
        public ActionResult Kids()
        {

            var kids = db.Subcategory.Where(x => x.category_id == 3).ToList();

            return PartialView(kids);
        }
        public ActionResult Stationery()
        {

            var stationery = db.Subcategory.Where(x => x.category_id == 4).ToList();

            return PartialView(stationery);
        }

        public ActionResult Shop(int? category_id,int? subcategory_id,int? page,int? id)//alt kategoriye göre ürün sıralama
        {
            var page_no = page ?? 1;
            var categories = db.Product.Where(x =>  x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 9);
            return View(categories);

        }
        public ActionResult Sign_up()
        {

            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sign_up([Bind(Include = "customer_id,first_name,last_name,username,password,telephone,e_mail")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customers);
        }
        public ActionResult MainPage()
        {

            return View();
        }

    }
}