using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private EFProductRepository repository;
        public AdminController(EFProductRepository repositoryPram)
        {
            this.repository = repositoryPram;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.GetAllBind());
        }
        [HttpGet]
        public ViewResult Edit(int ProductId)
        {
            Product product = repository.GetAll().First(U => U.ProductID == ProductId);

            return View(product);
        }
        [HttpPost]
        //public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.UpdateProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {                // there is something wrong with the data values 
                return View(product);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);

        }
        [HttpPost]
        public ActionResult Delete(int ProductId)
        {
            var MyProduct = repository.GetBy(ProductId);
            if (MyProduct != null)
            {
                repository.Remove(MyProduct);
                TempData["message"] = string.Format("{0} was deleted", MyProduct.Name);

            }
            return RedirectToAction("Index");


        }

    }
}