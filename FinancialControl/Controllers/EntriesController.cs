﻿using System;
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
    [Authorize]
    public class EntriesController : ControllerBase
    {
        private FinancialDbContext db = new FinancialDbContext();

        // GET: Entries
        [Authorize]
        public ActionResult Index(DateTime? startDate, DateTime? endDate, string categoria)
        {
            List<SelectListItem> categories = Categories.ToList();

            categories.Add(new SelectListItem());

            ViewBag.ListCat = categories.OrderBy(p => p.Text);

            if (endDate == null)
            {
                endDate = DateTime.Now.AddHours(1);
            }

            if (startDate == null)
            {
                startDate = DateTime.Now.AddDays(-30);
            }

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            if (String.IsNullOrEmpty(categoria))
            {
                ViewBag.categoriaDesc = String.Empty;

                if (Session["AccountId"] == null)
                {
                    var entries = from a in db.Entries where a.user == User.Identity.Name && a.dateMovement >= startDate && a.dateMovement <= endDate orderby a.dateMovement descending select a;
                    return View(entries);
                }
                else
                {
                    int account = (int)Session["AccountId"];
                    var entries = from a in db.Entries where a.account_id == account && a.user == User.Identity.Name && a.dateMovement >= startDate && a.dateMovement <= endDate orderby a.dateMovement descending select a;
                    return View(entries);
                }

            }
            else
            {
                ViewBag.categoriaDesc = _categories.FindLast(p => p.id == int.Parse(categoria)).categoryName;
                int intCategoria = int.Parse(categoria);

                if (Session["AccountId"] == null)
                {
                    var entries = from a in db.Entries where a.user == User.Identity.Name && a.dateMovement >= startDate && a.dateMovement <= endDate && a.category_id == intCategoria orderby a.dateMovement descending select a;
                    return View(entries);
                }
                else
                {
                    int account = (int)Session["AccountId"];
                    var entries = from a in db.Entries where a.account_id == account && a.user == User.Identity.Name && a.dateMovement >= startDate && a.dateMovement <= endDate && a.category_id == intCategoria orderby a.dateMovement descending select a;
                    return View(entries);
                }
            }
        }

        // GET: Entries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // GET: Entries/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = Categories.OrderBy(p => p.Text);
            ViewBag.ListAccount = Accounts.OrderBy(p => p.Value);

            Entries model = new Entries();

            model.dateMovement = DateTime.Now;
            model.user = User.Identity.Name;
            model.type = "D";

            return View(model);
        }

        private List<Category> _categories = null;
        private List<Account> _accounts = null;

        public IEnumerable<SelectListItem> Categories
        {
            get
            {
                _categories = db.CategoryModels.Where(p => p.user == User.Identity.Name).ToList();
                return new SelectList(_categories, "id", "categoryName");
            }
        }

        // POST: Entries/Create
        public IEnumerable<SelectListItem> Accounts
        {
            get
            {
                _accounts = db.Account.Where(p => p.user_mail == User.Identity.Name || p.account_id==1).ToList();
                return new SelectList(_accounts, "account_id", "account_description");
            }
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category_id,description,value,user,dateMovement, account_id")] Entries entries)
        {
            ViewBag.ListCat = Categories;
            ViewBag.ListAccount = Accounts.OrderBy(p => p.Value);

            if (ModelState.IsValid)
            {
                db.Entries.Add(entries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entries);
        }

        // GET: Entries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description,value,user,dateMovement,category_id, account_id")] Entries entries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entries);
        }

        // GET: Entries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entries entries = db.Entries.Find(id);
            db.Entries.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("Index");
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
