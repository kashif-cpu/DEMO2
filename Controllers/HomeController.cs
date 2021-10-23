using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcBankTask.Models;

namespace MvcBankTask.Controllers
{
    public class HomeController : Controller
    {
        aptechEntities Db = new aptechEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(ProppertyClass Pr)
        {
            var Login = Db.tbltransactions.Where(x => x.acc_no == Pr.accno).FirstOrDefault();

            if(Login == null)
            {
                ViewBag.Msg = "Sorry Invalid Account Number.....";
            }
            else
            {
                Session["Acc"] = Login.acc_no;
                return RedirectToAction("Index","Transactions"); 
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(ProppertyClass Pr)
        {
            if(Pr.dep_amount > 25000 || Pr.dep_amount < 0 || Pr.dep_amount%500 != 0)
            {
                ViewBag.Msg = "Enter Amount Like 500,1000,1500 Upto 25000 Only.....";
            }
            else if(Pr.gender == null)
            {
                ViewBag.Msg = "Please Select A Gender";
            }
            else if(Pr.accno.ToString().Length > 4 || Pr.accno.ToString().Count() < 4)
            {
                ViewBag.Msg = "Account Number Must Contain 4 Digits.....";
            }
            else
            {
                var CheckAccountNumber = Db.tblaccounts.Where(x => x.accno == Pr.accno).FirstOrDefault();

                if(CheckAccountNumber != null)
                {
                    ViewBag.Msg = "This Account Number Already Exists.....";
                }
                else
                {
                    tblaccount TblAc = new tblaccount();

                    TblAc.uname = Pr.uname;
                    TblAc.accno = Pr.accno;
                    TblAc.gender = Pr.gender;
                    TblAc.date_time = DateTime.Now.ToString();

                    Db.tblaccounts.Add(TblAc);

                    if(Db.SaveChanges() > 0)
                    {
                        tbltransaction TblTr = new tbltransaction();

                        TblTr.acc_no = Pr.accno;
                        TblTr.dep_amount = Pr.dep_amount;
                        TblTr.with_draw_amount = 0;
                        TblTr.current_bal = Pr.dep_amount;
                        TblTr.date_time = DateTime.Now.ToString();

                        Db.tbltransactions.Add(TblTr);

                        if(Db.SaveChanges() > 0)
                        {
                            ViewBag.Msg = "Account Created Successfully.....";
                        }

                    }

                }
            }
                

            return View();
        }     
        
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index","Home");
        }


    }
}