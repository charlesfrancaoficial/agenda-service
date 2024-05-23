using CSF.Entities;
using CSF.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Repository.SqlServer
{
    public class AppContext : DbContext, IAppContext
    {
        public AppContext()
            : base()
        { }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfiguration.GetConnectionString());
        }
    }

}
