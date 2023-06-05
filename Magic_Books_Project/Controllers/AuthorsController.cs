using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Magic_Books_Project.Models;

namespace Magic_Books_Project.Controllers
{
    public class AuthorsController : Controller
    {
        private magicdataEntities db = new magicdataEntities();

        // GET: Authors
        public async Task<ActionResult> Index(int? page)
        {
            return View(await db.Authors.ToListAsync());
        }
        public ActionResult author_edit_delete(int? id, int? page)//Product delete, edit 
        {
            var page_no = page ?? 1;
            var authors = db.Authors.ToList().ToPagedList(page_no, 10);
            return View(authors);

        }
        public ActionResult author_details(int? author_id, int? page)//list books by the same author
        {
            var page_no = page ?? 1;
            var author = db.Authors.Where(x => x.author_id == author_id);
            var related_author = db.Product.Where(x => x.author_id == author_id).ToList().ToPagedList(page_no, 10);
            ViewBag.rel_product = related_author;
            return View(related_author);

        }
        [HttpPost]
        public ActionResult Shoping(string price, int? page, int? subcategory_id, int? author_id)
        {
            var page_no = page ?? 1;

            if (price.Equals("UNDER 5"))
            {
                var products = db.Product.Where(x => x.price <= 5 && x.author_id == author_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("5 AND 10"))
            {
                var products = db.Product.Where(x => x.price >= 5 && x.price <= 10 && x.author_id == author_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("10 AND 25"))
            {
                var products = db.Product.Where(x => x.price >= 10 && x.price <= 25 && x.author_id == author_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("25 AND 50"))
            {
                var products = db.Product.Where(x => x.price >= 25 && x.price <= 50 && x.author_id == author_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("UPPER 50"))
            {
                var products = db.Product.Where(x => x.price >= 50 && x.author_id == author_id).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            return RedirectToAction("Shoping");

        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors authors = await db.Authors.FindAsync(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "author_id,author_name")] Authors authors)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(authors);
                await db.SaveChangesAsync();
                return RedirectToAction("author_edit_delete");
            }

            return View(authors);
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors authors = await db.Authors.FindAsync(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "author_id,author_name")] Authors authors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authors).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("author_edit_delete");
            }
            return View(authors);
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors authors = await db.Authors.FindAsync(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Authors authors = await db.Authors.FindAsync(id);
            db.Authors.Remove(authors);
            await db.SaveChangesAsync();
            return RedirectToAction("author_edit_delete");
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
