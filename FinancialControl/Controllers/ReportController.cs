using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialControl.Models;
using FinancialControl.Business;

namespace FinancialControl
{
    public class ReportController : ControllerBase
    {
        private FinancialDbContext db = new FinancialDbContext();

        // GET: Categories
        [Authorize]
        public ActionResult Expenses()
        {
            List<ReportExpenses> list = db.Database.SqlQuery<ReportExpenses>("SELECT * FROM V_EXPENSES WHERE VALUE < 0 AND MONTH = MONTH(GETDATE()) AND YEAR = YEAR(GETDATE()) and \"user\" = {0}",User.Identity.Name).ToList<ReportExpenses>();

            ViewBag.Total = list.Sum(P => P.Value).ToString("C");

            return View(list);
        }

        [Authorize]
        public ActionResult Installments()
        {
            List<InstallmentReport> list = db.Database.SqlQuery<InstallmentReport>("SELECT * FROM v_installments WHERE cast(ano as varchar(10)) + cast(format(mes,'00') as varchar(10)) >= cast(year(getdate()) as varchar(10)) + cast(format(month(getdate()),'00') as varchar(10)) and  \"user\" = {0} order by ano, mes", User.Identity.Name).ToList<InstallmentReport>();

            return View(list);
        }

        [Authorize]
        public ActionResult Netting(int? year)
        {
            ReportBusiness rep = new ReportBusiness();

            List<NettingClass> list = rep.getNettingReport(year,User.Identity.Name);

            ViewBag.Total = list.Sum(p => p.Netting).ToString("C");

            return View(list.OrderBy(p=>p.Year + p.Month));
        }

        // GET: Categories
        [Authorize]
        [HttpGet]
        public ActionResult ExpensesReport(string month, string year)
        {

            List<ReportExpenses> list;

            if (month != null)
            {
                 list = db.Database.SqlQuery<ReportExpenses>("SELECT * FROM V_EXPENSES WHERE VALUE < 0 AND MONTH = {0} AND YEAR = {1} and \"user\" = {2}", int.Parse(month), int.Parse(year), User.Identity.Name).ToList<ReportExpenses>();
                ViewBag.Month = int.Parse(month);
            }
            else
            {
                list = db.Database.SqlQuery<ReportExpenses>("SELECT * FROM V_EXPENSES WHERE VALUE < 0 AND MONTH = MONTH(GETDATE()) AND YEAR = YEAR(GETDATE()) and \"user\" = {0}",User.Identity.Name).ToList<ReportExpenses>();
            }


            ViewBag.Total = list.Sum(P => P.Value).ToString("C");

            return View(list);
        }
    }
}
