using PluralsightDdd.SharedKernel.Interfaces;
using PluralsightDdd.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.ScheduleAggregate
{
    public class Room : BaseEntity<int>, IAggregateRoot
    {
        public Room(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public Room UpdateName(string name)
        {
            Name = name;
            return this;
        }
    }
}
