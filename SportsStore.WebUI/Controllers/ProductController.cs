using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> repository;
        private int PageSize = 4;
        public ProductController(Repository<Product> repositoryPram)
        {
            this.repository = repositoryPram;
        }
        // GET: Product
        public ViewResult List(string category,int Page = 1)
        {
            var mylist = repository.GetAllBind()
                .Where(P=> category==null || P.Category== category)
                .OrderBy(p => p.ProductID)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize);

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = Page,
                ItemsPerPage = PageSize,
                TotalItems = category==null? repository.GetAllBind().Count():repository.GetAllBind().Where(c=>c.Category==category).Count()
            };
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = mylist,
                PagingInfo = pagingInfo,
                CurrentCategory= category

            };
            return View(model);
        }
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.GetAll().FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        
    }
}