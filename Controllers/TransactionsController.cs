using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBankTask.Models;

namespace MvcBankTask.Controllers
{
    public class TransactionsController : Controller
    {
        aptechEntities Db = new aptechEntities();

        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Deposite()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Deposite(tbltransaction Pr)
        {
            if(Pr.dep_amount < 0|| Pr.dep_amount > 25000 || Pr.dep_amount%500 != 0)
            {
                ViewBag.Msg = "Enter Amount Like 500,1000,1500 Upto 25000 Only.....";
            }
            else
            {
                int AccNumber = int.Parse(Session["Acc"].ToString());

                var GetMaxId = Db.tbltransactions.Where(x => x.acc_no == AccNumber).Max(x => x.trid);

                var GetCurrentBalanceRow = Db.tbltransactions.Where(x => x.trid == GetMaxId).FirstOrDefault();

                Pr.acc_no = AccNumber;
                Pr.with_draw_amount = 0;
                //Pr.dep_amount = Pr.dep_amount;

                var CurrentAmount = GetCurrentBalanceRow.current_bal;

                //CurrentAmount += Pr.dep_amount;

                CurrentAmount = CurrentAmount + Pr.dep_amount;

                Pr.current_bal = CurrentAmount;
                Pr.date_time = DateTime.Now.ToString();

                Db.tbltransactions.Add(Pr);

                if(Db.SaveChanges() > 0)
                {
                    ViewBag.Msg = "Amount Deposited Successfully.....";
                }
            }

            return View();
        }

        public ActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(tbltransaction Pr)
        {
            if (Pr.with_draw_amount < 0 || Pr.with_draw_amount > 25000 || Pr.with_draw_amount % 500 != 0)
            {
                ViewBag.Msg = "Enter Amount Like 500,1000,1500 Upto 25000 Only.....";
            }
            else
            {
                int AccNumber = int.Parse(Session["Acc"].ToString());

                var GetMaxId = Db.tbltransactions.Where(x => x.acc_no == AccNumber).Max(x => x.trid);

                var GetCurrentBalanceRow = Db.tbltransactions.Where(x => x.trid == GetMaxId).FirstOrDefault();

                var CurrentAmount = GetCurrentBalanceRow.current_bal;

                if(Pr.with_draw_amount > CurrentAmount)
                {
                    ViewBag.Msg = "The Current Amount Is Less Than Given Amount, Kindly Check Current Amount..";
                }
                else
                {
                    // CurrentAmount -= Pr.with_draw_amount;
                    CurrentAmount = CurrentAmount - Pr.with_draw_amount;

                    Pr.acc_no = AccNumber;
                    Pr.dep_amount = 0;
                    Pr.current_bal = CurrentAmount;
                    Pr.date_time = DateTime.Now.ToString();

                    Db.tbltransactions.Add(Pr);

                    if(Db.SaveChanges() > 0)
                    {
                        ViewBag.Msg = "Amount WithDrawn Successfully.....";
                    }
                }
            }

            return View();
        }

    }
}