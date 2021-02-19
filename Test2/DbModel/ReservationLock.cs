using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.DbModel
{
    public class ReservationLock
    {
        public int Email { get; set; }
        public DateTime ReservationDate { get; set; }
        public int Timeslot { get; set; }
        public int ReservedTables { get; set; }
        public DateTime LockTime{ get; set; }
        public string Status { get; set; }
    }
}
