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
    public class SubcategoriesController : Controller
    {
        private magicdataEntities db = new magicdataEntities();

        // GET: Subcategories
        public async Task<ActionResult> Index()
        {
            var subcategory = db.Subcategory.Include(s => s.Categories);
            return View(await subcategory.ToListAsync());
        }
        public ActionResult subcategory_edit_delete(int? id, int? page)//Product delete, edit 
        {
            var page_no = page ?? 1;
            var subcategory = db.Subcategory.ToList().ToPagedList(page_no, 10);
            return View(subcategory);

        }
        // GET: Subcategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = await db.Subcategory.FindAsync(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: Subcategories/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");
            return View();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "subcategory_id,subcategory_name,category_id")] Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Subcategory.Add(subcategory);
                await db.SaveChangesAsync();
                return RedirectToAction("subcategory_edit_delete");
            }

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name", subcategory.category_id);
            return View(subcategory);
        }

        // GET: Subcategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = await db.Subcategory.FindAsync(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name", subcategory.category_id);
            return View(subcategory);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "subcategory_id,subcategory_name,category_id")] Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("subcategory_edit_delete");
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name", subcategory.category_id);
            return View(subcategory);
        }

        // GET: Subcategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = await db.Subcategory.FindAsync(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Subcategory subcategory = await db.Subcategory.FindAsync(id);
            db.Subcategory.Remove(subcategory);
            await db.SaveChangesAsync();
            return RedirectToAction("subcategory_edit_delete");
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
