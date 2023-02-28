using PluralsightDdd.SharedKernel.Interfaces;
using PluralsightDdd.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.ScheduleAggregate
{
    public class Doctor : BaseEntity<int>, IAggregateRoot
    {
        public Doctor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
