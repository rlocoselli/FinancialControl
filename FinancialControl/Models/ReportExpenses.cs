using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinancialControl.Models
{
    [Table("v_expenses")]
    public class ReportExpenses
    {
        [Display(Name = "Category", ResourceType = typeof(App_GlobalResources.st))]
        public string Category { get; set; }
        
        [Display(Name ="Value", ResourceType = typeof(App_GlobalResources.st))]
        public decimal Value { get; set; }

        [Display(Name = "Average", ResourceType = typeof(App_GlobalResources.st))]
        public decimal? Average { get; set; }

        [Display(Name = "Month", ResourceType = typeof(App_GlobalResources.st))]
        public int Month { get; set; }

        [Display(Name = "Year", ResourceType = typeof(App_GlobalResources.st))]
        public int Year { get; set; }

        [Display(Name = "User", ResourceType = typeof(App_GlobalResources.st))]
        public string User { get; set; }

        [Display(Name = "Percentage_Period", ResourceType = typeof(App_GlobalResources.st))]
        public decimal Percentage_Period { get; set; }
    }
}