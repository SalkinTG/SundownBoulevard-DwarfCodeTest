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
        public int Id { get; }
        [EmailAddress]
        public string Mail { get; set; }
        [Range(1, 10)]
        public int NumberOfPersons { get; set; }
        public DateTime Date = DateTime.Today;
        public int? Timeslot { get; set; }
        public string DrinkName { get; set; }
        public string Dish { get; set; }
        public string Status { get; set; }

        public Booking()
        {
        }


        public Booking(int id, string mail, int numberOfPersons, DateTime date, int timeslot, string drinkId, string dish)
        {
            Id = id;
            Mail = mail;
            NumberOfPersons = numberOfPersons;
            Date = date;
            Timeslot = timeslot;
            DrinkName = drinkId;
            Dish = dish;
            Status = "Accepted";
        }

        public Booking BookingFromDb(int id)
        {
            using var context = new Model();
            var dbObject = context.Bookings.Single(
                b => b.Id == id
                );
            int maxPersons = 2 * dbObject.ReservedTables;
            Booking booking = new Booking(dbObject.Id, dbObject.Email, maxPersons, dbObject.ReservationDate, dbObject.Timeslot, dbObject.DrinkId, dbObject.Dish);
            booking.Status = dbObject.Status;
            return booking;
        }
        public List<Booking> GetBookingsFromMail(string email)
        {
            List<Booking> bookings = new List<Booking>(0);
            using var context = new Model();
            List<int> ids = context.Bookings.Where(b => b.Email == email && b.Status == "active").Select(b => b.Id).ToList();

            foreach (var id in ids)
            {
                bookings.Add(BookingFromDb(id));
            }
            return bookings;
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
                DrinkId = DrinkName,
                Dish = Dish
            };
            context.Add(dbObject);
            context.SaveChanges();
        }

        public void TimeslotSelected()
        {
            UnlockReservation();

            int[] availableSeats = GetAvailableSeats();

            if (availableSeats[(int)Timeslot] >= NumberOfPersons)
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
