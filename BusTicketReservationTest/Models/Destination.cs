using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class Destination
    {
        public Destination()
        {
            Tickets = new HashSet<Ticket>();
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string StartTown { get; set; } = null!;
        public string EndTown { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
