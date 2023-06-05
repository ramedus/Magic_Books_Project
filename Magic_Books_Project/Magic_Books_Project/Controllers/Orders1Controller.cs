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

namespace Magic_Books_Project.Controllers
{
    public class Orders1Controller : Controller
    {
        private magicdataEntities db = new magicdataEntities();

        // GET: Orders1
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Customers).Include(o => o.Product);
            return View(await orders.ToListAsync());
        }

        // GET: Orders1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders1/Create


        public async Task<ActionResult> Create([Bind(Include = "order_id,user_id,product_id,order_date,order_number,quantity")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.Customers, "customer_id", "first_name", orders.user_id);
            ViewBag.product_id = new SelectList(db.Product, "product_id", "name", orders.product_id);
            return View(orders);
        }
        public ActionResult order(string product_name, int? price, int? quantity,int? product_id,int? customer_id)
        {
            var prodct = db.Product.Where(x => x.name == product_name);
            var customer = ((Customers)Session["members"]).customer_id;
            var new_order_no = db.Orders.Max(x => x.order_id);
            
            new_order_no = new_order_no + 1;
            return RedirectToAction("Create",
                   new
                   {
                       
                       user_id = customer,
                       product_id = prodct.Select(x => x.product_id),
                       order_date = DateTime.Today,
                       order_number = new_order_no,
                       quantity = quantity,   
                   });

            return View();
        }

        // GET: Orders1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Customers, "customer_id", "first_name", orders.user_id);
            ViewBag.product_id = new SelectList(db.Product, "product_id", "name", orders.product_id);
            return View(orders);
        }

        // POST: Orders1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "order_id,user_id,product_id,order_date,order_number,quantity")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Customers, "customer_id", "first_name", orders.user_id);
            ViewBag.product_id = new SelectList(db.Product, "product_id", "name", orders.product_id);
            return View(orders);
        }

        // GET: Orders1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Orders orders = await db.Orders.FindAsync(id);
            db.Orders.Remove(orders);
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
