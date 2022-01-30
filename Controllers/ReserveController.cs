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
    public class ReserveController : Controller
    {
        // GET: Reserve
        public ActionResult Reserve()
        {

            Member member = (Member)Session["Member"];

            if (member != null)
            {
                ViewBag.Name1 = "logined";

            }

            if (member == null)
            {
                return RedirectToAction("Login", "Account");

            }

            ReserveModel rm = new ReserveModel();
            ReserveTable rt = new ReserveTable();

            rm.Reserveflag = false;

            rm.GetReserveData = rt.GetReserve(member);

            if(rm.GetReserveData.Rows.Count != 0)
            {
                rm.Reservedflag = true;
            }
            else
            {
                rm.Reservedflag = false;
            }

            return View(rm);
        }
        [HttpPost]
        public ActionResult Send(Reserve r)
        {
            Reserve rv = new Reserve();
            ReserveModel rm = new ReserveModel();
            ReserveTable rt = new ReserveTable();
            Member memberSession = (Member)Session["Member"];
            String ReId = r.TableNo.ToString();

            if (r.TableNo <= 9)
                ReId = "0" + r.TableNo.ToString();


            rv.MemberId = memberSession.MemberId;
            rv.Num = r.Num;
            rv.ReserveDateTime = r.ReserveDateTime;
            rv.ReserveId = r.ReserveDateTime.ToString().Substring(5,2) + r.ReserveDateTime.ToString().Substring(8, 2) + r.ReserveDateTime.ToString().Substring(11, 2) + r.ReserveDateTime.ToString().Substring(14, 2) + ReId;
            rv.TableNo = r.TableNo;


        

            rt.Insert(rv);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Reserve(DateTime s1, String s2, String s3)
        {
            String str = s1.ToString().Substring(0, 10) + " " + s2 + ":" + s3;

            DateTime dt = DateTime.Parse(str);
            ReserveModel rm = new ReserveModel();
            ReserveTable rt = new ReserveTable();
            rm.GetReserveData = rt.GettableId(dt);
            rm.Reserveflag = true;
            rm.ReserveDateTime = DateTime.Parse(s1.ToString().Substring(0, 10) + "T" + s2 + ":" + s3);
            rm.ReservedTable = rt.GethourReserve(dt);


            return View(rm);
        }

        [HttpPost]
        public ActionResult Delete(string Reid)
        {
            ReserveModel rm = new ReserveModel();
            ReserveTable rt = new ReserveTable();

            Member memberSession = (Member)Session["Member"];

            rt.Delete(Reid);

            return RedirectToAction("Reserve", "Reserve");
        }
        [HttpPost]
        public ActionResult Update(string num, string Reid)
        {
            ReserveModel rm = new ReserveModel();
            ReserveTable rt = new ReserveTable();

            Member memberSession = (Member)Session["Member"];

            int temp =int.Parse(num);

            rt.Update(temp,Reid);

            return RedirectToAction("Reserve", "Reserve");
        }
    }
}