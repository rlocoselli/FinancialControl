using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FinancialControl
{
    public class ControllerBase : Controller
    {
        public List<Account> Accounts
        {
            get
            {
                FinancialDbContext db = new FinancialDbContext();
                List<Account> accounts = db.Account.Where(p => p.user_mail == User.Identity.Name || p.account_id == 1).ToList();
                return accounts;
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (requestContext.HttpContext.Request.UserLanguages.Count() > 0)
            {
                string lang = requestContext.HttpContext.Request.UserLanguages[0];
                CultureInfo ci = CultureInfo.GetCultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = ci;
            }

            ViewBag.Accounts = Accounts;

            try
            {
                if (String.IsNullOrEmpty((string)Session["AccountName"]))
                {
                    Session["AccountName"] = FinancialControl.App_GlobalResources.st.All;
                }
                else
                {
                    if (Request.QueryString["accountIdContext"] != null && Request.QueryString["accountIdContext"] != "")
                    {
                        Session["AccountName"] = Accounts.First(p => p.account_id == int.Parse(Request.QueryString["accountIdContext"])).account_description;
                        Session["AccountId"] = int.Parse(Request.QueryString["accountIdContext"]);
                    }
                    else
                    {
                        if (Request.QueryString["accountIdContext"] == "0")
                        {
                            Session["AccountId"] = null;
                            Session["AccountName"] = FinancialControl.App_GlobalResources.st.All;
                        }
                    }

                    ((List<Account>)ViewBag.Accounts).Add(new Account() { account_description = FinancialControl.App_GlobalResources.st.All, account_id=0 });
                }
            }
            catch
            {
                Session["AccountId"] = null;
                Session["AccountName"] = FinancialControl.App_GlobalResources.st.All;
            }
            ViewBag.AccountName = Session["AccountName"];

        }
    }
}