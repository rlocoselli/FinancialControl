using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinancialControl.Models
{
    [Table("v_netting")]
    public class NettingClass
    {
        [Display(Name = "Debt")]
        public decimal Debt { get; set; }
        
        [Display(Name ="Credit")]
        public decimal Credit { get; set; }

        [Display(Name = "Netting")]
        public decimal Netting { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}