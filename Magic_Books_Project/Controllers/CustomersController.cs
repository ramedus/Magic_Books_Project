using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magic_Books_Project.Models;
using PagedList;

namespace Magic_Books_Project.Controllers
{
    public class CustomersController : Controller
    {
        private magicdataEntities db = new magicdataEntities();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }
        public ActionResult customers_edit_delete(int? id, int? page)//Product delete, edit 
        {
            var page_no = page ?? 1;
            var customers = db.Customers.ToList().ToPagedList(page_no, 10);
            return View(customers);

        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "customer_id,first_name,last_name,username,password,telephone,e_mail")] Customers customers)
        {
            try
            {
                db.Customers.Add(customers);
                await db.SaveChangesAsync();
                ViewBag.kayit_durum = "Kayıt Başarılı";
            }
            catch (Exception hata)
            {
                int yeri = hata.InnerException.ToString().IndexOf("uk_customers_username");
                if (yeri != -1)
                    ViewBag.kayit_durum = "Böyle Email var Kayıt Yapılamıyor";
                else
                {
                    ViewBag.kayit_durum = "Böyle kuladi var değiştirin var Kayıt Yapılamıyor";
                    
                    
                }
            }

            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "customer_id,first_name,last_name,username,password,telephone,e_mail")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("customers_edit_delete");
            }
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customers customers = await db.Customers.FindAsync(id);
            db.Customers.Remove(customers);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
