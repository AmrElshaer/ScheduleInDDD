using ScheduleInDDD.Core.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.Interfaces
{
    public interface IScheduleRepository
    {
        Schedule GetScheduleForDate(int clinicId, DateTime date);
        void Update(Schedule schedule);
    }
}
