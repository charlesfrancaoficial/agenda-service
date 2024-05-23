using CSF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Repository.SqlServer
{
    public static class Seeder
    {
        public static void Seed(this AppContext salesContext)
        {
            if (!salesContext.Appointments.Any())
            {
                //--- The next two lines add 100 rows to your database
                List<Appointment> users = new List<Appointment> {
                    new Appointment {
                        Id = Guid.NewGuid(),
                        UserId = Guid.NewGuid().ToString(),
                        ProfessionalUserId = Guid.NewGuid().ToString(),
                        Notes = "consulta agendada",
                        Date = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };
                salesContext.AddRange(users);
            }

            salesContext.SaveChanges();
        }
    }
}
