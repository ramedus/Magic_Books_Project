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
    public class CategoriesController : Controller
    {
        private magicdataEntities db = new magicdataEntities();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }
        public ActionResult categories_edit_delete(int? id, int? page)//Category delete, edit 
        {
            var page_no = page ?? 1;
            var category = db.Categories.ToList().ToPagedList(page_no, 10);
            return View(category);

        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "category_id,name")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                await db.SaveChangesAsync();
                return RedirectToAction("categories_edit_delete");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "category_id,name")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("categories_edit_delete");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categories categories = await db.Categories.FindAsync(id);
            db.Categories.Remove(categories);
            await db.SaveChangesAsync();
            return RedirectToAction("categories_edit_delete");
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
