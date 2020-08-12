using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        //private Repository<Product> repository;
        private EFProductRepository repository;
        public NavController(EFProductRepository repositorypram)
        {
            this.repository = repositorypram;
        }
        // GET: Nav
        //public PartialViewResult Menu(string category = null)
        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {

            ViewBag.SelectedCategory = category;

            var list = repository.categories();
            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, list);
        }
    }
}