using JecPizza.Models;
using JecPizza.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace JecPizza.Controllers
{
    public class DeliveryController : Controller
    {
        // GET: Delivery
        public ActionResult Delivery()
        {

            Member memberSession = (Member)Session["Member"];

            if (memberSession != null)
            {
                ViewBag.Name1 = "logined";

            }

            return View();
        }
        [HttpPost]
        public ActionResult Buy(DateTime date, string address)
        {
            Member member = (Member)Session["Member"];
            Cart cart = (Cart)Session["Cart"];
            int total = (int)Session["total"];

            Purchase pu = new Purchase();

            pu.MemId = member.MemberId;
            pu.CartId = cart.CartId;
            pu.PurchaseDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            if (address == null)
                pu.Address = member.Address;
            else
                pu.Address = address;
            pu.DeliveryDate = date;
            pu.Total = total;

            

            return RedirectToAction("Index", "Home");
        }
    }
}