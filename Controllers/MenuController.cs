using System.Web.Mvc;
using JecPizza.Models;
using JecPizza.Models.Repositories;

namespace JecPizza.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Menu()
        {

            Member memberSession = (Member)Session["Member"];

            if (memberSession != null)
            {
                ViewBag.Name1 = "logined";

            }
            GoodsModel gm = new GoodsModel();
            GoodsTable gt = new GoodsTable();

            gm.GoodsDataTable = gt.GetGoodsDataTable();
            gm.NewGoodsTab = gt.GetNewGoods();
            gm.RecGoodsDataTable = gt.GetRecGoods();
            gm.SearchGoodsDataTable = null;

            return View(gm);
        }
        [HttpPost]
        public ActionResult Menu(string str)
        {
            GoodsModel gm = new GoodsModel();
            GoodsTable gt = new GoodsTable();

            gm.GoodsDataTable = gt.GetGoodsDataTable();
            gm.NewGoodsTab = gt.GetNewGoods();
            gm.RecGoodsDataTable = gt.GetRecGoods();
            gm.SearchGoodsDataTable = gt.SearchGoods(str);

            ViewBag.data1 = str;

            return View(gm);
        }
    }
}