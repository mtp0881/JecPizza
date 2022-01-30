using JecPizza.Models;
using JecPizza.Models.Repositories;
using System;
using System.Web.Mvc;

namespace JecPizza.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
            public ActionResult Register()
            {

            Member memberSession = (Member)Session["Member"];

            if (memberSession != null)
            {
                ViewBag.Name1 = "logined";

            }

            Member member = (Member)Session["Member"];
            if (member != null)
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                return View("Register");
            }

        }

        [HttpPost]
        public ActionResult Register(MemberRegisterModel model)
        {
            Member member = model;
            MemberTable mt = new MemberTable();
            member.MemberId = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + new Random((int)DateTime.Now.Ticks).Next(1000000).ToString("D6");
            

            if (mt.Insert(member) != 0)
            {
                return View("RegisterEnd");
            }
            else
            {
                return View();
            }

        }
    }
}