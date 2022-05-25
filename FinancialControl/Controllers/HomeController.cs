using FinancialControl.Business;
using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FinancialControl.Controllers
{
    public class HomeController : ControllerBase
    {
        private FinancialDbContext db = new FinancialDbContext();

        private DateTime GetLastDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public ActionResult Index()
        {
            try
            {
                if (User.Identity.Name != null)
                {

                    DateTime hoy = DateTime.Today;

                    decimal? credit = db.Entries.Where(p => p.user == User.Identity.Name && p.Category.type == "C" && p.dateMovement.Month == DateTime.Now.Month && p.dateMovement.Year == DateTime.Now.Year).Sum(p => p.value);
                    decimal? debt = db.Entries.Where(p => p.user == User.Identity.Name && p.Category.type == "D" && p.dateMovement.Month == DateTime.Now.Month && p.dateMovement.Year == DateTime.Now.Year).Sum(p => p.value);
                    decimal? dayExpense = db.Entries.Where(p => p.user == User.Identity.Name && p.Category.type == "D" && System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(p.dateMovement) == hoy).Sum(p => p.value);
                    decimal? dayScheduled = db.Entries.Where(p => p.user == User.Identity.Name && p.Category.type == "D" && System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(p.dateMovement) == hoy && p.schedule_id != null).Sum(p => p.value);
                    int? countScheduled = db.Entries.Where(p => p.user == User.Identity.Name && p.Category.type == "D" && System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(p.dateMovement) == hoy && p.schedule_id != null).Count();

                    credit = credit == null ? 0 : credit;
                    debt = debt == null ? 0 : debt;
                    dayExpense = dayExpense == null ? 0 : dayExpense;

                    decimal? netting = credit - debt;

                    if(db.CategoryModels.Where(p=>p.user==User.Identity.Name).Count() == 0)
                    {
                        ViewBag.NoCategories = true;
                    }
                    else
                    {
                        ViewBag.NoCategories = false;
                    }

					if (db.Entries.Where(p => p.user == User.Identity.Name).Count() == 0)
					{
						ViewBag.NoEntries = true;
					}
					else
					{
						ViewBag.NoEntries = false;
					}

					ViewBag.DayExpenses = String.Format("{0:c}", dayExpense.Value);

                    if (dayScheduled != null)
                        ViewBag.DayScheduled = String.Format("{0:c}", dayScheduled.Value);
                    else
                        ViewBag.DayScheduled = String.Format("{0:c}", 0);

                    ReportBusiness rep = new ReportBusiness();

                    List<NettingClass> list = rep.getNettingReport(DateTime.Now.Year, User.Identity.Name);

                    ViewBag.TotalOfTheYear = list.Sum(p => p.Netting).ToString("C");
                    ViewBag.DueToday = countScheduled > 0;

                    ViewBag.Credit = String.Format("{0:c}", credit.Value);
                    ViewBag.Debit = String.Format("{0:c}", debt.Value);
                    ViewBag.Netting = String.Format("{0:c}", netting.Value);
                    ViewBag.Days = Math.Round(GetLastDayOfMonth(GetLastDayOfMonth(DateTime.Now)).Subtract(DateTime.Now).TotalDays,0);
                }
            }
            catch
            {

            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}