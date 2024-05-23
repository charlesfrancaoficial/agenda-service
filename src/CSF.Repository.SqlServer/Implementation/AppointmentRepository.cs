using CSF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Repository.SqlServer.Implementation
{
    public class AppointmentRepository : BaseRepository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(IUnitOfWork unit) : base(unit)
        {

        }
    }

}
