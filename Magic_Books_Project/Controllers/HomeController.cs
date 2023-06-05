using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Magic_Books_Project.Models;
using System.IO;
using PagedList;
using System.Windows;

namespace Magic_Books_Project.Controllers
{
    public class HomeController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        public ActionResult Index()
        {
            var new_books = db.Product.OrderByDescending(x => x.published_date);

            return View(new_books.Take(8));
        }
        public ActionResult About()
        {

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult Login()
        {
            
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(Customers members)
        {
            var admin = db.Customers.FirstOrDefault(x => members.username == "admin" && members.password == "admin");

            
            var member = db.Customers.FirstOrDefault(x => x.username == members.username && x.password == members.password);
            if (member != null)
            {
                Session["members"] = member;
            }
            else if (admin != null)
            {

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                MessageBox.Show("Wrong username or password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return RedirectToAction("Login", "Home");
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
        public ActionResult Romance()
        {

            var romance = db.Subcategory.Where(x => x.subcategory_id == 8).ToList();

            return RedirectToAction("Shoping", new { subcategory_id = 8 });
        }
        public ActionResult KidsE()
        {

            var romance = db.Categories.Where(x => x.category_id == 3).ToList();

            return RedirectToAction("Shoping", new { category_id = 3 });
        }
        public ActionResult Fantasy()
        {
            var romance = db.Subcategory.Where(x => x.subcategory_id == 9).ToList();

            return RedirectToAction("Shoping", new { subcategory_id = 9 });
        }
        public ActionResult Classics()
        {
            var romance = db.Subcategory.Where(x => x.subcategory_id == 1).ToList();

            return RedirectToAction("Shoping", new { subcategory_id = 1 });
        }
        public ActionResult Poetry()
        {
            var romance = db.Subcategory.Where(x => x.subcategory_id == 7).ToList();

            return RedirectToAction("Shoping", new { subcategory_id = 7 });
        }
        public ActionResult FictionCategory()
        {
            var category = db.Categories.Where(x => x.category_id == 1).ToList();

            return RedirectToAction("Shoping", new { category_id = 1 });
        }
        public ActionResult NonfictionCategory()
        {
            var category = db.Categories.Where(x => x.category_id == 2).ToList();

            return RedirectToAction("Shoping", new { category_id = 2 });
        }
        public ActionResult KidsCategory()
        {
            var category = db.Categories.Where(x => x.category_id == 3).ToList();

            return RedirectToAction("Shoping", new { category_id = 3 });
        }
        public ActionResult StationeryCategory()
        {
            var category = db.Categories.Where(x => x.category_id == 4).ToList();

            return RedirectToAction("Shoping", new { category_id = 4 });
        }

        public ActionResult Shoping(int? category_id, int? subcategory_id, int? page, int? id)//alt kategoriye göre ürün sıralama
        {
            var page_no = page ?? 1;
            if(category_id != null)
            {

                var categories1 = db.Product.Where(x => x.category_id ==category_id).ToList().ToPagedList(page_no, 12);
                return View(categories1);


            }
            var categories = db.Product.Where(x => x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 12);
            return View(categories);

        }
        [HttpPost]
        public ActionResult Shoping(string price, int? page, int? subcategory_id)
        {
            var page_no = page ?? 1;

            if (price.Equals("UNDER 5"))
            {
                var products = db.Product.Where(x => x.price <= 5 && x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("5 AND 10"))
            {
                var products = db.Product.Where(x => x.price >= 5 && x.price <= 10 && x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("10 AND 25"))
            {
                var products = db.Product.Where(x => x.price >= 10 && x.price <= 25 && x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("25 AND 50"))
            {
                var products = db.Product.Where(x => x.price >= 25 && x.price <= 50 && x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("UPPER 50"))
            {
                var products = db.Product.Where(x => x.price >= 50 && x.subcategory_id == subcategory_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            return RedirectToAction("Shoping");

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
                return RedirectToAction("Login");
            }

            return View(customers);
        }
        public ActionResult new_books()
        {
            var new_books = db.Product.OrderByDescending(x => x.published_date);
            
            return View(new_books.Take(8));
        }
        public ActionResult logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}