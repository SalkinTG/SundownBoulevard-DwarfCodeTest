using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking
{
    public class Booking
    {
        public string Mail { get; }
        public int NumberOfPersons { get; }
        public DateTime Date { get; }
        public int Timeslot { get; }
        public int DrinkId { get; }
        public string Dish { get; }

        public Booking(string Mail, int NumberOfPersons, DateTime Date, int Timeslot, int DrinkId, string Dish)
        {
            this.Mail = Mail;
            this.NumberOfPersons = NumberOfPersons;
            this.Date = Date;
            this.Timeslot = Timeslot;
            this.DrinkId = DrinkId;
            this.Dish = Dish;
        }

        public void SaveBooking()
        {
        }
    }
}
