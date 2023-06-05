using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Magic_Books_Project.Models;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagedList;
using System.Windows;
using System.Net;
using System.Data.Entity;

namespace Magic_Books_Project.Controllers
{
    public class ProductsController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        // GET: Products

        public ActionResult Index(int? category_id, int? subcategory_id, int? page, int? id)//Page showing all products
        {
            var page_no = page ?? 1;
            var categories = db.Product.ToList().ToPagedList(page_no, 12);
            return View(categories);

        }
        [HttpPost]
        public ActionResult Index (string price, int? page)//Price filter
        {
            var page_no = page ?? 1;

            if (price.Equals("UNDER 5"))
            {
                var products = db.Product.Where(x => x.price <= 5).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("5 AND 10"))
            {
                var products = db.Product.Where(x => x.price >= 5 && x.price <= 10).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("10 AND 25"))
            {
                var products = db.Product.Where(x => x.price >= 10 && x.price <= 25).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("25 AND 50"))
            {
                var products = db.Product.Where(x => x.price >= 25 && x.price <= 50).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            if (price.Equals("UPPER 50"))
            {
                var products = db.Product.Where(x => x.price >= 50).ToList().ToPagedList(page_no, 12);
                return View(products);
            }
            return RedirectToAction("Index");
        }

        public ActionResult product_edit_delete(int? id, int? page)//Product delete, edit 
        {
            var page_no = page ?? 1;
            var products = db.Product.ToList().ToPagedList(page_no, 10);
            return View(products);

        }

        public async Task<ActionResult> product_save()//Add Product
        {
            ViewBag.subcategory_id = new SelectList(db.Subcategory, "subcategory_id", "subcategory_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "name");
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> product_save(Product new_product, HttpPostedFileBase file)// Add Product httpPost
        {

           string file_name = "no_img.png";

            if (file != null)
            {
                if (file.ContentLength > 0)//dosya transfer olduysa
                {
                    String extension = Path.GetExtension(file.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        file_name = Path.GetFileName(file.FileName);
                        String path = Path.Combine(Server.MapPath("~/images"), file_name);
                        file.SaveAs(path);

                        ViewBag.situation = "Picture transferred and information saved.";
                    }
                    else
                    {
                        ViewBag.situation = "You need to select image file. Wrong type.";
                    }
                }
                else
                {
                    ViewBag.situation = "Information saved without a picture.";
                }
            }
            else
            {
                MessageBox.Show("You cannot save a product. Error!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            new_product.image = file_name;
            if (new_product.category_id == 4)
            {
                new_product.publisher_id = null;
                new_product.author_id = null;
                db.Product.Add(new_product);
            }
            else
            {
                db.Product.Add(new_product);
            }
            
            db.SaveChanges();
            ViewBag.subcategory_id = new SelectList(db.Subcategory, "subcategory_id", "subcategory_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "name");
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");
            return RedirectToAction("product_edit_delete");
        }
        public ActionResult product_details(int? product_id)//Product details
        {

            var products = db.Product.Where(x => x.product_id == product_id);
            var related_product = db.Product.Where(x => x.category_id == ((Product)products).subcategory_id);
            ViewBag.rel_product = related_product;
            return View(products);

        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Product.FindAsync(id);
            db.Product.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("product_edit_delete");
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.subcategory_id = new SelectList(db.Subcategory, "subcategory_id", "subcategory_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "name");
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");
            return View(product);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "product_id,name,price,stock,image,description,subcategory_id,isbn,total_pages,published_date,publisher_id,author_id,category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("product_edit_delete");
            }
            ViewBag.subcategory_id = new SelectList(db.Subcategory, "subcategory_id", "subcategory_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "name");
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");
            return View(product);
        }

    }
}