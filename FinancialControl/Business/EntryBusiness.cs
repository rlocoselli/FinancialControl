using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace FinancialControl.Business
{
    public class EntryBusiness
    {        
        public bool DeleteSchedule(Schedule schedule, FinancialDbContext db)
        {
            try
            {
                List<Entries> entries = db.Entries.Where(p => p.schedule_id == schedule.id && EntityFunctions.TruncateTime(p.dateMovement) >= EntityFunctions.TruncateTime(DateTime.Now)).ToList();

                entries.ForEach(p => db.Entries.Remove(p));

                db.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }            
        }

        public bool InsertSchedule(Schedule schedule, FinancialDbContext db)
        {
            try
            {
                int qtdRecords = schedule.quantity_installment != null ? schedule.quantity_installment.Value : 36;
                bool parcelado = schedule.quantity_installment != null;
                List<Entries> list = new List<Entries>();

                for (int i = 0; i < qtdRecords; i++)
                {
                    Entries entry = new Entries()
                    {
                        category_id = schedule.category_id,
                        dateMovement = schedule.start_movement.AddMonths(i),
                        description = String.Format("{0} {1}-{2}", schedule.description, i + 1, qtdRecords),
                        value = schedule.value,
                        user = schedule.user,
                        schedule_id = schedule.id
                    };

                    list.Add(entry);
                }

                db.Entries.AddRange(list);
                db.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}