using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialControl.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("v_installments")]
    public class InstallmentReport
    {
        [Display(Name = "Month", ResourceType = typeof(App_GlobalResources.st))]
        public int mes { get; set; }
        
        [Display(Name = "Year", ResourceType = typeof(App_GlobalResources.st))]
        public int ano { get; set; }

        [Display(Name = "Value", ResourceType = typeof(App_GlobalResources.st))]
        public decimal? amount { get; set; }
    }
}