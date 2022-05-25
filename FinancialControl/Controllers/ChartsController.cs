using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialControl.Models;
using System.Web.UI.WebControls;

namespace FinancialControl
{
    public class ChartsController : ControllerBase
    {
        private FinancialDbContext db = new FinancialDbContext();

        [Authorize]
        public ActionResult ExpensesPerCat()
        {
            return View();
        }

        [Authorize]
        public ActionResult ExpensesPerMonth()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult ExpensesPerCatChart(string month, string year)
        {
            ChartModel cm = new ChartModel();
            cm.X = new List<string>();
            cm.Y = new List<decimal>();

            List<ExpensePerCat> epm = db.Database.SqlQuery<ExpensePerCat>("select top 5 Category,sum(abs(value)) value from v_expenses where type = 'D' and month={0} and year = {1} and \"user\" = {2}  group by category order by value desc",month,year,User.Identity.Name).ToList();

            epm.ForEach(p => cm.X.Add(p.Category));
            epm.ForEach(p => cm.Y.Add(p.Value));

            return View(cm);
        }

        [Authorize]
        public ActionResult ExpensesPerMonthChart()
        {
            ChartModel cm = new ChartModel();
            cm.X = new List<string>();
            cm.Y = new List<decimal>();

            List<ExpensePerMonth> epm = db.Database.SqlQuery<ExpensePerMonth>("select cast(month as varchar) + '/' + cast(year as varchar) month,sum(abs(value)) value from v_expenses where type = 'D' and \"user\" = {0} and year = year(getdate()) group by month, year",User.Identity.Name).ToList();

            epm.ForEach(p => cm.X.Add(p.Month));
            epm.ForEach(p => cm.Y.Add(p.Value));

            return View(cm);
        }
    }
}
