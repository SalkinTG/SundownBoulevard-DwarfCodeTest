using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbModel;
using FluentAssertions.Common;

namespace Server.Booking
{
    public class Booking
    {
        private string Mail { get; set; }
        private int NumberOfPersons { get; set; }
        private DateTime Date { get; set; }
        private int Timeslot { get; set; }
        private int DrinkId { get; set; }
        private string Dish { get; set; }
        private string Status { get; set; }

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
            Booking booking = new Booking(dbObject.Email, dbObject.Persons, dbObject.Date, dbObject.Timeslot, dbObject.Drink, dbObject.Dish);
            booking.Status = dbObject.Status;
            return booking;
        }
        public void SaveBooking()
        {
            using var context = new Model();
            var dbObject = new BookingTable
            {
                Email = Mail,
                Persons = NumberOfPersons,
                Date = Date,
                Timeslot = Timeslot,
                Drink = DrinkId,
                Dish = Dish
            };
            context.Add(dbObject);
            context.SaveChanges();
        }
    }
}
