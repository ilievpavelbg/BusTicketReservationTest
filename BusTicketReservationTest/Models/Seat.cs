using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class Seat
    {
        public Seat()
        {
            Buses = new HashSet<bus>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsBooked { get; set; }

        public virtual ICollection<bus> Buses { get; set; }
    }
}
