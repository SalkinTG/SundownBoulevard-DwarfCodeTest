using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test2.DbModel;
using FluentAssertions.Common;
using System.ComponentModel.DataAnnotations;
using Test2.Booking;

namespace Test2.CancelBooking
{
    public class CancelBooking
    {
        [EmailAddress]
        public string Email;

        public void Cancel(int id)
        {
            using var context = new Model();

            var booking = context.Bookings.SingleOrDefault(c => c.Id == id);
            if (booking != null)
            {
                booking.Status = "Cancelled";
                context.SaveChanges();
            }
        }
    }
}
