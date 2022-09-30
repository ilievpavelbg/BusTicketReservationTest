using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Destinations = new HashSet<Destination>();
        }

        public int Id { get; set; }
        public string StartDate { get; set; } = null!;
        public string StartHour { get; set; } = null!;
        public double Duration { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
