using System.Web.Mvc;
using JecPizza.Models;
using JecPizza.Models.Repositories;

namespace JecPizza.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            GoodsModel gm = new GoodsModel();
            GoodsTable gt = new GoodsTable();
            ToppingCartTable tct = new ToppingCartTable();

            gm.GoodsDataTable = gt.GetGoodsDataTable();
            gm.NewGoodsTab = gt.GetNewGoods();
            gm.RecGoodsDataTable = gt.GetRecGoods();

            ToppingCart tc = new ToppingCart()
            {
                TpCId = "TK0001"
            };

            Member member = (Member)Session["Member"];

            if (member != null)
            {
                ViewBag.Name1 = "logined";

            }

            return View(gm);
        }

        public ActionResult Search(string str)
        {
            return RedirectToAction("Login", "Menu");
        }
    }
}