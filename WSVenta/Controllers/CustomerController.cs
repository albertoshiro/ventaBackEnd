using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Response;

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
                    var list = db.Customer.ToList();
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




    }
}
