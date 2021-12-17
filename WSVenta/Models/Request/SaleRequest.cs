using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSVenta.Models.Request
{
    public class SaleRequest
    {
        //la fecha se calculara en el server
        [Required]
        //rango de valores
        [Range (1, Double.MaxValue,ErrorMessage = "El valor asignado deve de ser mayor a 0  ")]
        [CustomerExist(ErrorMessage = "El cliente no existe en la bd")]
        public int IdCustomer {get; set;}
        //public decimal Total { get; set; }
        //ocupar los tipos de dato necesarios

        [Required]
        //este MinLeght nos dice el minimo de lementos 
        [MinLength(1,ErrorMessage ="Deven Existir conceptos al menos uno")]
        public List<Concepto> Conceptos { get; set; }

        //solo inicializamos la lista en vacia por si se da el caso en que no tenga articulos o venga null 
        public SaleRequest()
        {
            this.Conceptos = new List<Concepto>();
        }
    }

    public class Concepto
    {
        public int Amount{ get; set; }
        public decimal UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public int IdProduct { get; set; }

    }

    #region
    public class CustomerExistAttribute : ValidationAttribute
    {
        //este value nos trae el tipo de objeto en donde es invocado este data anotation, esta es la forma en la que puedes generar tus propios data anotation
        public override bool IsValid(object value)
        {
            //estas casteando esta variable a un int y lo asignas a la variable idcliente
            int idCliente = (int)value;
            using (var db = new VENTA_REALContext())
            {   //con esto podrias evaluar dado lo que te traiga este qwery
                //var objExist = db.Customer.Where(d => d.Id == idCliente).FirstOrDefault();
                //tambien puedes ponerlo directo 
                if (db.Customer.Find(idCliente) == null) return false;
            }
                return true;
        }
    }
    #endregion
}
