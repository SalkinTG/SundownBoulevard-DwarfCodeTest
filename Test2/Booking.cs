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
        public int Timeslot { get; set; }
        public int DrinkId { get; set; }
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

        public void ChangeDate(DateTime date)
        {
            Date = date;
        }

        public Task ChangePersons(int numberOfPersons)
        {
            NumberOfPersons = numberOfPersons;
            return Task.CompletedTask;
        }

        public void ChangeTimeslot(int timeslot)
        {
            Timeslot = timeslot;
        }

        public void ChangeDrink(int drinkId)
        {
            DrinkId = drinkId;
        }
        public void ChangeDish(string dish)
        {
            Dish = dish;
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
        public void SaveBooking()
        {
            using var context = new Model();
            var dbObject = new BookingTable
            {
                Email = Mail,
                ReservedTables = NumberOfPersons,
                ReservationDate = Date,
                Timeslot = Timeslot,
                DrinkId = DrinkId,
                Dish = Dish
            };
            context.Add(dbObject);
            context.SaveChanges();
        }

        public void ReservationLock(string mail, DateTime reservationDate, int timeslot, int persons)
        {

        }

        public void GetAvailableSeats(DateTime date)
        {
            using var context = new Model();
            var query = from ts in context.Bookings
                        where ts.ReservationDate.Date == date.Date
                        select ts.Timeslot;
            var booked = context.Bookings.Where(
                ts => ts.ReservationDate.Date == date.Date)
                .Select(ts => new
            {
                Time = ts.Timeslot,
                Number = ts.ReservedTables
            }).ToList();
            var locked = context.ReservationLocks.Where(
                ts => ts.ReservationDate.Date == date.Date && 
                ts.LockTime.AddMinutes(5) == DateTime.Now && 
                ts.Status == "Active")
                .Select(ts => new
            {
                Time = ts.Timeslot,
                Number = ts.ReservedTables
            }).ToList();
        }
    }
}
