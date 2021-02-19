using System.ComponentModel.DataAnnotations;
using System;

namespace Test2.DbModel
{
    public class BookingTable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public int Timeslot { get; set; }
        [Required]
        public int ReservedTables { get; set; }
        [Required]
        public int DrinkId { get; set; }
        [Required]
        public string Dish { get; set; }
        public string Status { get; set; }
    }
}
