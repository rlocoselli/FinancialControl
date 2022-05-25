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
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (requestContext.HttpContext.Request.UserLanguages.Count() > 0)
            {
                string lang = requestContext.HttpContext.Request.UserLanguages[0];
                CultureInfo ci = CultureInfo.GetCultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            
        }
    }
}