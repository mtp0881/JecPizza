using System.Web.Mvc;
using JecPizza.Models;
using JecPizza.Models.Repositories;

namespace JecPizza.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Account()
        {
            Member member = (Member)Session["Member"];

            if (member == null)
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                AccountModel accountModel = new AccountModel();

                MemberTable mb = new MemberTable();

                accountModel.AccountData = member;

                accountModel.AccountPurchaseData = mb.AccountPurchaseData(member);

                ViewBag.Name1 = "logined";

                return View(accountModel);
            }
        }

        // GET: Account
        public ActionResult Login()
        {
            Session["Member"] = null;

            ViewBag.Name1 = "logout";

            LoginModel model = new LoginModel();

            model.message = "";

            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            Member member = new Member
            {
                MemberId = model.id,
                Password = model.passWord
            };

            MemberTable memberTable = new MemberTable();

            if (memberTable.LoginMember(member))
            {
                Session["Member"] = memberTable.GetMember(member);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.message = "会員IDまたはパスワードに誤りがあります";
                return View("Login", model);
            }
        }

        public ActionResult RedirectEntry()
        {
            return RedirectToAction("Register", "Register");
        }

        public ActionResult MemberEntry()
        {
            MemberRegisterModel memberRegisterModel = new MemberRegisterModel();
            return View("Register");
        }


        [HttpPost]
        public ActionResult MemberEntry(MemberRegisterModel model)
        {
            Member member = (Member)model;
            MemberTable memberTable = new MemberTable();
            if (memberTable.GetMember(member) == null)
            {
                memberTable.Insert(member);
                return View("RegisterEnd");
            }
            else
            {
                
                return View("RegisterEnd", model);
            }
        }

        public ActionResult EntryEnd()
        {
            return View();
        }

        public ActionResult RedirectLogin()
        {
            return RedirectToAction("Login");
        }
    }
}