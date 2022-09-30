using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
