using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class bus
    {
        public bus()
        {
            Tickets = new HashSet<Ticket>();
            Seats = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public string RegNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public int BusOwnerId { get; set; }

        public virtual BusOwner BusOwner { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
