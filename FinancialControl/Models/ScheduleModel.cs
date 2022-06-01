using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FinancialControl.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("schedule")]
    public class Schedule
    {
        [Key]
        [Required]
        [Display(Name = "Code", ResourceType = typeof(App_GlobalResources.st))]
        public int id { get; set; }

        [Required]
        [Display(Name = "Account", ResourceType = typeof(App_GlobalResources.st))]
        public int account_id { get; set; }

        [ForeignKey("account_id")]
        public virtual Account Account { get; set; }

        [Required(ErrorMessageResourceName = "CategoryRequired", ErrorMessageResourceType = typeof(App_GlobalResources.st))]
        [Display(Name = "Category", ResourceType = typeof(App_GlobalResources.st))]
        public int category_id { get; set; }

        [ForeignKey("category_id")]
        public virtual Category Category { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionRequired", ErrorMessageResourceType = typeof(App_GlobalResources.st))]
        [Display(Name = "DescriptionField", ResourceType = typeof(App_GlobalResources.st))]
        public string description { get; set; }

        [Display(Name = "Value", ResourceType = typeof(App_GlobalResources.st))]
        public decimal? value { get; set; }

        [Required(ErrorMessageResourceName = "ValueRequired", ErrorMessageResourceType = typeof(App_GlobalResources.st))]
        [Display(Name = "Total", ResourceType = typeof(App_GlobalResources.st))]
        public decimal? total { get; set; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(App_GlobalResources.st))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime start_movement { get; set; }

        [Required]
        [Display(Name = "EndDate", ResourceType = typeof(App_GlobalResources.st))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime end_movement
        {
            get
            {
                FinancialDbContext db = new FinancialDbContext();

                if (this.id > 0)
                    return db.Entries.Where(p => p.schedule_id == this.id).Max(p => p.dateMovement);
                else
                    return DateTime.MaxValue;
            }
        }
        [NotMapped]
        [Display(Name = "Flg_installment", ResourceType = typeof(App_GlobalResources.st))]
        public bool flg_installment { get { return quantity_installment != null && quantity_installment > 0; } }

        [NotMapped]
        [Display(Name = "Flg_installment", ResourceType = typeof(App_GlobalResources.st))]
        public int flg_installment2 { get { return flg_installment ? 1 : 0;  } }

        [Display(Name = "Quantity_installment", ResourceType = typeof(App_GlobalResources.st))]
        public int? quantity_installment { get; set; }

        [Required]
        [Display(Name = "User", ResourceType = typeof(App_GlobalResources.st))]
        public string user { get; set; }

    }
}
