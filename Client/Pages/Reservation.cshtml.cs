using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Client.Pages
{
    public class ReservationModel : PageModel
    {
        public Booking.Booking newBooking;

        public void OnGet()
        {
            StartBooking();
        }
        public void StartBooking()
        {
            newBooking = new Booking.Booking();
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
