using System;
using System.Collections.Generic;

namespace BusTicketReservationTest.Models
{
    public partial class BusOwner
    {
        public BusOwner()
        {
            buses = new HashSet<bus>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Vatnumber { get; set; } = null!;
        public string Town { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string WebSite { get; set; } = null!;

        public virtual ICollection<bus> buses { get; set; }
    }
}
