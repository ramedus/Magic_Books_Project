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
    public class PublishersController : Controller
    {
        private magicdataEntities db = new magicdataEntities();

        // GET: Publishers
        public async Task<ActionResult> Index()
        {
            return View(await db.Publishers.ToListAsync());
        }
        public ActionResult publishers_edit_delete(int? id, int? page)//Product delete, edit 
        {
            var page_no = page ?? 1;
            var publishers = db.Publishers.ToList().ToPagedList(page_no, 10);
            return View(publishers);

        }

        // GET: Publishers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publishers publishers = await db.Publishers.FindAsync(id);
            if (publishers == null)
            {
                return HttpNotFound();
            }
            return View(publishers);
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "publisher_id,name")] Publishers publishers)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(publishers);
                await db.SaveChangesAsync();
                return RedirectToAction("publishers_edit_delete");
            }

            return View(publishers);
        }

        // GET: Publishers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publishers publishers = await db.Publishers.FindAsync(id);
            if (publishers == null)
            {
                return HttpNotFound();
            }
            return View(publishers);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "publisher_id,name")] Publishers publishers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publishers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("publishers_edit_delete");
            }
            return View(publishers);
        }

        // GET: Publishers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publishers publishers = await db.Publishers.FindAsync(id);
            if (publishers == null)
            {
                return HttpNotFound();
            }
            return View(publishers);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Publishers publishers = await db.Publishers.FindAsync(id);
            db.Publishers.Remove(publishers);
            await db.SaveChangesAsync();
            return RedirectToAction("publishers_edit_delete");
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
