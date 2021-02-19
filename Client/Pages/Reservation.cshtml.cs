using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Server.Booking;

namespace Client.Pages
{
    public class ReservationModel : PageModel
    {
        public string Mail;
        public int Persons;
        public DateTime Date;
        public int Timeslot;
        public int Drink;
        public string Dish;

        public void SetData()
        {
            Mail = "a@b.c";
        }

        public Booking newBooking;

        public void OnGet()
        {
            StartBooking();
        }
        public void StartBooking()
        {
            newBooking = new Booking();
        }

        public void PickDate(DateTime date)
        {
            newBooking.ChangeDate(date);
        }

        public void PickNumberOfPersons(int numberOfPersons)
        {
            newBooking.ChangePersons(numberOfPersons);
        }

        public void PickTimeslot(int timeslot)
        {
            newBooking.ChangeTimeslot(timeslot);
        }

        public void PickDrink(int drinkId)
        {
            newBooking.ChangeDrink(drinkId);
        }

        public void PickDish(string dish)
        {
            newBooking.ChangeDish(dish);
        }

        public void MakeReservation()
        {
            newBooking.SaveBooking();
        }
    }
}
