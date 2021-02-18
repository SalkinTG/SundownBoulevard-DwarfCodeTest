using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbModel
{
    public class Model : DbContext
    {
        public DbSet<BookingTable> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Filename=DBModel/Bookings.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingTable>()
                .Property(b => b.Status)
                .HasDefaultValue("active");
        }
    }
}
