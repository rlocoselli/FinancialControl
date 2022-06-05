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
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private FinancialDbContext db = new FinancialDbContext();
        private List<Account> _accounts = null;

        // GET: Schedule/ScheduleIndex
        public ActionResult ScheduleIndex()
        {
            IQueryable<Schedule> query = db.Schedule.Where(p=>p.user == User.Identity.Name);

            if (Session["AccountId"] != null)
            {
                int account = (int)Session["AccountId"];
                query = query.Where(p => p.account_id == account);
            }

            IQueryable<Entries> query2 = db.Entries.Where(p => p.user == User.Identity.Name && p.dateMovement > DateTime.Now);

            var query3 = from s in query
                        join s2 in query2 on s.id equals s2.schedule_id
                        select s;

            return View(query3.Distinct().ToList());
        }

        public IEnumerable<SelectListItem> Accounts
        {
            get
            {
                _accounts = db.Account.Where(p => p.user_mail == User.Identity.Name || p.account_id == 1).ToList();
                return new SelectList(_accounts, "account_id", "account_description");
            }
        }

        /*
        // GET: Schedule/ScheduleDetails/5
        public ActionResult ScheduleDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }
        */

        // GET: Schedule/ScheduleCreate
        public ActionResult ScheduleCreate()
        {
            Schedule schedule = new Schedule();

            schedule.start_movement = DateTime.Now;
            schedule.user = User.Identity.Name;

            ViewBag.category_id = new SelectList(db.CategoryModels.Where(a => a.user == User.Identity.Name).OrderBy(p=>p.categoryName).ToList(), "id", "categoryName");
            ViewBag.ListAccount = Accounts.OrderBy(p => p.Value);

            return View(schedule);
        }

        // POST: Schedule/ScheduleCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ScheduleCreate([Bind(Include = "Category,id,category_id,description,value,total,start_movement,flg_installment,quantity_installment,user, account_id")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                if (schedule.flg_installment)
                {
                    schedule.value = schedule.total.Value / schedule.quantity_installment;
                }
                else
                {
                    schedule.value = schedule.total.Value;
                }

                db.Schedule.Add(schedule);
                db.SaveChanges();

                EntryBusiness business = new EntryBusiness();

                if (!business.InsertSchedule(schedule,db))
                {
                    db.Schedule.Remove(schedule);
                    DisplayErrorMessage();
                    return View(schedule);
                }

                DisplaySuccessMessage("Has append a Schedule record");
                return RedirectToAction("ScheduleIndex");
            }

            ViewBag.category_id = new SelectList(db.CategoryModels.Where(a => a.user == User.Identity.Name).OrderBy(p => p.categoryName).ToList(), "id", "categoryName");
            ViewBag.ListAccount = Accounts.OrderBy(p => p.Value);

            DisplayErrorMessage();
            return View(schedule);
        }

        // GET: Schedule/ScheduleEdit/5
        public ActionResult ScheduleEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.CategoryModels.Where(a => a.user == User.Identity.Name).OrderBy(p => p.categoryName).ToList(), "id", "categoryName");
            ViewBag.ListAccount = Accounts.OrderBy(p => p.Value);
            return View(schedule);
        }

        // POST: ScheduleSchedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult ScheduleEdit([Bind(Include = "Category,id,category_id,description,value,dateMovement,flg_installment,quantity_installment,user")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Schedule record");
                return RedirectToAction("ScheduleIndex");
            }
            ViewBag.category_id = new SelectList(db.CategoryModels.Where(a => a.user == User.Identity.Name).OrderBy(p => p.categoryName).ToList(), "id", "categoryName");
            ViewBag.ListAccount = Accounts.OrderBy(p => p.Value);

            DisplayErrorMessage();
            return View(schedule);
        }

        // GET: Schedule/ScheduleDelete/5
        public ActionResult ScheduleDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/ScheduleDelete/5
        [HttpPost, ActionName("ScheduleDelete")]
        public ActionResult ScheduleDeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedule.Find(id);

            EntryBusiness business = new EntryBusiness();

            if (!business.DeleteSchedule(schedule, db))
            {
                DisplayErrorMessage();
                return View(schedule);
            }

            db.Schedule.Remove(schedule);
            db.SaveChanges();

            DisplaySuccessMessage("Has delete a Schedule record");
            return RedirectToAction("ScheduleIndex");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
