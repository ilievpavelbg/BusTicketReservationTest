using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int AgeDiscount { get; set; }
        public string? Comments { get; set; }
        public int BookingId { get; set; }
        public int BusId { get; set; }
        public int DestinationId { get; set; }
        public bool? IsValid { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual bus Bus { get; set; } = null!;
        public virtual Destination Destination { get; set; } = null!;
    }
}
