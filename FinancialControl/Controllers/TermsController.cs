using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialControl.Controllers
{
	public class TermsController : ControllerBase
	{
		// GET: Terms
		public ActionResult Index()
		{
			return View();
		}
	}
}
