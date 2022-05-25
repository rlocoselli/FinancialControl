using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinancialControl.Models
{
    [Table("entries")]
    public class Entries
    {
        [Key]
        [Required]
        [Display(Name = "Code", ResourceType = typeof(App_GlobalResources.st))]
        public int id { get; set; }

        [Display(Name = "Schedule", ResourceType = typeof(App_GlobalResources.st))]
        public int? schedule_id { get; set; }

        [Required(ErrorMessageResourceName="CategoryRequired",ErrorMessageResourceType=typeof(App_GlobalResources.st))]
        [Display(Name = "Category", ResourceType = typeof(App_GlobalResources.st))]
        public int category_id { get; set; }

        [ForeignKey("category_id")]
        public virtual Category Category { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionRequired", ErrorMessageResourceType = typeof(App_GlobalResources.st))]
        [StringLength(30)]
        [Display(Name = "DescriptionField", ResourceType = typeof(App_GlobalResources.st))]
        public string description { get; set; }

        [Display(Name = "Type", ResourceType = typeof(App_GlobalResources.st))]
        public string type { get; set; }

        [Required(ErrorMessageResourceName = "ValueRequired", ErrorMessageResourceType = typeof(App_GlobalResources.st))]
        [Display(Name = "Value", ResourceType = typeof(App_GlobalResources.st))]
        public decimal? value { get; set; }

        [Required]
        [Display(Name = "Date", ResourceType = typeof(App_GlobalResources.st))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateMovement { get; set; }

        [Required]
        [Display(Name = "User", ResourceType = typeof(App_GlobalResources.st))]
        public string user { get; set; }
    }
}