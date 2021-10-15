using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSVenta.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Concept = new HashSet<Concept>();
        }

        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int? IdCustomer { get; set; }
        public decimal? Total { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual ICollection<Concept> Concept { get; set; }
    }
}
