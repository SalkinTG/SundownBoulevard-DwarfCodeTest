using System.ComponentModel.DataAnnotations;
using System;

namespace DbModel
{
    public class BookingTable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Timeslot { get; set; }
        [Required]
        public int Persons { get; set; }
        [Required]
        public int Drink { get; set; }
        [Required]
        public string Dish { get; set; }
        public string Status { get; set; }
    }
}
