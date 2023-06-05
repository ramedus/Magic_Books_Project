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
    public class ProductsController : Controller
    {
        magicdataEntities db = new magicdataEntities();
        // GET: Products
        public ActionResult Shop(int? category_id, int? subcategory_id, int? page, int? id)//tüm ürünleri gösteren sayfa
        {
            var page_no = page ?? 1;
            var categories = db.Product.ToList().ToPagedList(page_no, 5);
            return View(categories);

        }
        public ActionResult Index(int? id, int? page)//ürün kaydedince açılan sayfa
        {
            var page_no = page ?? 1;
            var products = db.Product.ToList().ToPagedList(page_no, 5);
            return View(products);

        }
        public async Task<ActionResult> product_save()//ürün kaydet
        {
            ViewBag.subcategory_id = new SelectList(db.Subcategory, "subcategory_id", "subcategory_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "name");
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> product_save(Product new_product, HttpPostedFileBase file)
        {

           string file_name = "janeeyre.png";

            if (file != null)
            {
                if (file.ContentLength > 0)//dosya transfer olduysa
                {
                    String extension = Path.GetExtension(file.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        file_name = Path.GetFileName(file.FileName);
                        String path = Path.Combine(Server.MapPath("~/product_pics"), file_name);
                        file.SaveAs(path);

                        ViewBag.situation = "Picture transferred and information saved.";
                    }
                    else
                    {
                        ViewBag.durum = "You need to select image file. Wrong type.";
                    }
                }
                else
                {
                    ViewBag.durum = "Information saved without a picture.";
                }
            }
            new_product.image = file_name;
            db.Product.Add(new_product);
            db.SaveChanges();
            ViewBag.subcategory_id = new SelectList(db.Subcategory, "subcategory_id", "subcategory_name");
            ViewBag.publisher_id = new SelectList(db.Publishers, "publisher_id", "name");
            ViewBag.author_id = new SelectList(db.Authors, "author_id", "author_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "name");
            return RedirectToAction("Index");
        }


    }
}