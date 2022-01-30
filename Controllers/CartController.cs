using System;
using System.Web.Mvc;
using JecPizza.Models;
using JecPizza.Models.Repositories;

namespace JecPizza.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {

            Member memberSession = (Member)Session["Member"];

            if (memberSession != null)
            {
                ViewBag.Name1 = "logined";

            }

            CartTable ct = new CartTable();
            CardTable cd = new CardTable();


            Member member = (Member)Session["Member"];
            if (member == null)
            {
                return RedirectToAction("Login", "Account");

            }

            CartModel cm = new CartModel
            {
                GetMemberCart = ct.GetCartDataTable(member),
                GetMemberCard = cd.GetCardDataTable(member)
                
            };
            return View(cm);

        }


        [HttpPost]
        public ActionResult Cart(string goodsCode)
        {

            Member member = (Member)Session["Member"];
            if (member == null)
                return RedirectToAction("Login", "Account");

            GoodsTable gt = new GoodsTable();
            Goods goods = gt.GetGoods(goodsCode);

            CartTable ct = new CartTable();
            Cart cart = new Cart()
            {
                GoodsId = goods.GoodsId,
                Num = 1,
                CartId = ct.GetCartId(member) ??  new Random().Next(1000).ToString("d4"),
                MemberId = member.MemberId
        };

            ct.Insert(cart);

            Session["Cart"] = cart;


            return RedirectToAction("Cart", "Cart");
        }

        public ActionResult Update(string num, string goodsCode)
        {
            CartTable ct = new CartTable();
            Cart cart = new Cart();
            Member member = (Member)Session["Member"];

            cart.GoodsId = goodsCode;
            cart.Num = int.Parse(num);
            cart.MemberId = member.MemberId;
            ct.Update(cart);

            return RedirectToAction("Cart");
        }
        public ActionResult Delete(string goodsCode)
        {
            CartTable ct = new CartTable();
            Cart cart = new Cart();
            Member member = (Member)Session["Member"];

            cart.GoodsId = goodsCode;
            cart.MemberId = member.MemberId;
            ct.Delete(cart);

            return RedirectToAction("Cart");
        }
    }
}