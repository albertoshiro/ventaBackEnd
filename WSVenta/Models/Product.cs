using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSVenta.Models
{
    public partial class Product
    {
        public Product()
        {
            Concept = new HashSet<Concept>();
        }

        public int Id { get; set; }
        public byte[] Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Concept> Concept { get; set; }
    }
}
