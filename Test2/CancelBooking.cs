using System.Linq;
using Test2.DbModel;
using System.ComponentModel.DataAnnotations;

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
