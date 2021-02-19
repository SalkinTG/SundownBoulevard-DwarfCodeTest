using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test2.DbModel;
using FluentAssertions.Common;
using System.ComponentModel.DataAnnotations;

namespace Test2.Booking
{
    public class Booking
    {
        [EmailAddress]
        public string Mail { get; set; }
        [Range(1, 10)]
        public int NumberOfPersons { get; set; }
        public DateTime Date = DateTime.Today;
        public int? Timeslot { get; set; }
        public int? DrinkId { get; set; }
        public string Dish { get; set; }
        public string Status { get; set; }

        public Booking()
        {
        }


        public Booking(string mail, int numberOfPersons, DateTime date, int timeslot, int drinkId, string dish)
        {
            Mail = mail;
            NumberOfPersons = numberOfPersons;
            Date = date;
            Timeslot = timeslot;
            DrinkId = drinkId;
            Dish = dish;
            Status = "Accepted";
        }

        public Booking BookingFromDb(int id)
        {
            using var context = new Model();
            var dbObject = context.Bookings.Single(
                b => b.Id == id
                );
            Booking booking = new Booking(dbObject.Email, dbObject.ReservedTables, dbObject.ReservationDate, dbObject.Timeslot, dbObject.DrinkId, dbObject.Dish);
            booking.Status = dbObject.Status;
            return booking;
        }

        public int PersonsToTables(int persons)
        {
            int tables = persons / 2;
            return tables;
        }
        public void SaveBooking()
        {
            using var context = new Model();
            var dbObject = new BookingTable
            {
                Email = Mail,
                ReservedTables = PersonsToTables(NumberOfPersons),
                ReservationDate = Date,
                Timeslot = (int)Timeslot,
                DrinkId = (int)DrinkId,
                Dish = Dish
            };
            context.Add(dbObject);
            context.SaveChanges();
        }

        public void TimeslotSelected()
        {
            UnlockReservation();

            int[] availableSeats = GetAvailableSeats();

            if (availableSeats[(int)Timeslot] <= NumberOfPersons)
            {
                ReservationLock();
            } else
            {
                throw new Exception("not enough seats available at the selected timslot");
            }
        }

        public void ConfirmBooking()
        {
            SaveBooking();
            UnlockReservation();
        }

        public void ReservationLock()
        {
            using var context = new Model();
            var dbObject = new ReservationLock
            {
                Email = Mail,
                ReservedTables = PersonsToTables(NumberOfPersons),
                ReservationDate = Date,
                Timeslot = (int)Timeslot,
                LockTime = DateTime.Now,
                Status = "Active"
            };
            context.Add(dbObject);
            context.SaveChanges();
        }

        public void UnlockReservation()
        {
            using var context = new Model();
            var query = from rl in context.ReservationLocks
                        where rl.Email == Mail
                        select rl;
            context.ReservationLocks.RemoveRange(query);
        }

        public int[] GetAvailableSeats()
        {
            int[] availableSeats = new int[3];
            using var context = new Model();

            for(int i = 0; i < availableSeats.Length; i++)
            {
                int booked = context.Bookings.Where(
                ts => ts.ReservationDate.Date == Date.Date && 
                ts.Timeslot == i)
                .Sum(n => n.ReservedTables);

                int locked = context.ReservationLocks.Where(
                ts => ts.ReservationDate.Date == Date.Date &&
                ts.LockTime.AddMinutes(5) == DateTime.Now &&
                ts.Status == "Active" &&
                ts.Timeslot == i)
                .Sum(n => n.ReservedTables);

                int available = 10 - booked - locked;
                availableSeats[i] = 2*available;
            }

            return availableSeats;
        }
    }
}
