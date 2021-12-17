using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Models;
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        //en este caso para injectar dependencias podemos ocupar interfaces, para poder ocupar cualquier tipo de objeto que ejecute esta interfaz, 
        private ISaleService _sale ;
        public SaleController(ISaleService sale)
        {
            this._sale = sale;
        }

        [HttpPost]
        public IActionResult Add(SaleRequest model)
        {
            Response response = new Response();
            try
            {
                //en este caso se saco la funcionalidad del success y demas 
                _sale.Add(model);
                response.Success = 1;
            }
            catch ( Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

    }
}
