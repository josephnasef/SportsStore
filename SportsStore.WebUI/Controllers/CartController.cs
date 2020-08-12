using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Manger;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private Repository<Product> repository;
        private IOrderProcessor orderProcessor;
        public CartController(IOrderProcessor proc, Repository<Product> repositoryPram)
        {
            this.repository = repositoryPram;
            orderProcessor = proc;
        }

        // GET: Cart
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {

                ReturnUrl = returnUrl,
                Cart = cart
            });
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public RedirectToRouteResult AddToCart(Cart cart, int ProductId, string returnUrl)
        {
            var MyProduct = repository.GetAll().FirstOrDefault(p => p.ProductID == ProductId);
            if (MyProduct != null)
            {
                cart.AddItem(MyProduct, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCard(Cart cart, int ProductId, string returnUrl)
        {
            var MyProduct = repository.GetAll().FirstOrDefault(p => p.ProductID == ProductId);
            if (MyProduct != null)
            {
                cart.Remove(MyProduct);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}