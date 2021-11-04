using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Response;
using WSVenta.Models.Request;

namespace WSVenta.Controllers
{
    //ruta
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //especificamos que entraremos por get a este metodo
        [HttpGet]
        public IActionResult Get()
        {
            var oResponse = new Response();
            oResponse.Success = 0;
            //creamos un objeto de ñla clase dbcontext, esto digamos realiza nuestra conexion a la bd y podremos sacar los datos de este objeto como si fueran consultas
            using (var db = new VENTA_REALContext())
            {
                try
                {
                    //en este caso creamos una lista , sacada de los registros del objeto db.Customer(es decir los clientes), y los convertimos en una lista
                    var list = db.Customer.OrderByDescending(d=>d.Id).ToList();
                    oResponse.Success = 1;
                    oResponse.Data = list;
                    //este metodo Ok regresa un objeto que implementa la interfaz IactionResult
                    //return Ok(list);
                }
                catch (Exception ex)
                {
                    oResponse.Message = ex.Message;

                }
                return Ok(oResponse);
            } 
        }

        [HttpPost]
        public IActionResult Add(CustomerRequest oModel)
        {
            var oResponse = new Response();
            oResponse.Success = 0;
            try
            {
                using (var db = new VENTA_REALContext())
                {
                    Customer oCustomer = new Customer();
                    oCustomer.Name = oModel.Name;

                    db.Customer.Add(oCustomer);
                    //con esto se guardan los cambios lo de arriba es solo la instruccion
                    db.SaveChanges();
                    oResponse.Success = 1;
                }                
            }
            catch ( Exception ex)
            {
                oResponse.Message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpPut]
        public IActionResult Edit( CustomerRequest oModel)
        {
            var oResponse = new Response();
            oResponse.Success = 0;
            try
            {

                using(var db = new VENTA_REALContext())
                {
                    var oCustomer = db.Customer.Find(oModel.Id);
                    oCustomer.Name = oModel.Name;
                    db.Entry(oCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oResponse.Success = 1;
                }                
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }
            return Ok(oResponse);
        }

        //el siguiente metodo http request nos pide un id, este sera pasada por medio de un espacio en la base de la url , aqui se pasara el id para saber que registro deve de eliminar, este se pondra /6 al final de la url por ejemplo indicando que por este medio se le pasara el parametro
        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var oResponse = new Response();
            oResponse.Success = 0;
            try
            {
                using (var db = new VENTA_REALContext())
                {
                    //buscas un objeto que tenga el mismo id que el que le pasamos
                    var oCustomer = db.Customer.Find(id);
                    //remueves un objeto dado antes 
                    db.Customer.Remove(oCustomer);
                    db.SaveChanges();
                     oResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }
            return Ok(oResponse);
        }
    }
}
