using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialControl.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("category")]
    public class Category
    {
        [Key]
        [Required]
        [Display(Name = "Code", ResourceType = typeof(App_GlobalResources.st))]
        public int id { get; set; }
        
        [Required]
        [Display(Name = "Name", ResourceType = typeof(App_GlobalResources.st))]
        public string categoryName { get; set; }

        [Display(Name = "User", ResourceType = typeof(App_GlobalResources.st))]
        public string user { get; set; }

        [Required]
        [Display(Name = "Type", ResourceType = typeof(App_GlobalResources.st))]
        public string type { get; set; }

    }
}