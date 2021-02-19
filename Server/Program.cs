using System;
using Server.Booking;

namespace Server
{
    class Program
    {
        static void Main()
        {
            Booking.Booking booking = new Booking.Booking("a@b.c", 1, DateTime.Now, 1, 1, "food");

            booking.SaveBooking();
        }
    }
}
