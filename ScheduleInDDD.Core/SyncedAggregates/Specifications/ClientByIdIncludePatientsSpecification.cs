using Ardalis.Specification;
using ScheduleInDDD.Core.ScheduleAggregate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.SyncedAggregates.Specifications
{
    public class ClientByIdIncludePatientsSpecification : Specification<Client>, ISingleResultSpecification
    {
        public ClientByIdIncludePatientsSpecification(int clientId)
        {
            Query
              .Include(client => client.Patients)
              .Where(client => client.Id == clientId);
        }
    }
}
