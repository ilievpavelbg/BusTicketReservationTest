using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string BookingNumber { get; set; } = null!;
        public string? Description { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
