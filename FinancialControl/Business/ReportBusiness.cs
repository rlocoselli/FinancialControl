using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialControl.Business
{
    public class ReportBusiness
    {
        private FinancialDbContext db = new FinancialDbContext();

        public List<NettingClass> getNettingReport(int? year, string user)
        {
            if (year == null)
                year = DateTime.Now.Year;

            List<NettingClass> list = db.Database.SqlQuery<NettingClass>("SELECT * FROM V_NETTING where \"user\" = {0} AND YEAR={1} ORDER BY YEAR, MONTH", user, year).ToList<NettingClass>();

            return list;

        }

    }
}