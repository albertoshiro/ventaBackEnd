using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //buena practica a los atributos privados se le pone guion bajo, como tal en esta linea inyectar en el controlador
        private IUserService _userService;

        //en esta parte se podria decir que injectas el el servicio , un objeto userService en el argumento privado de la misma clase, como que pasas la estafeta de algo de la misma classe o interfaz
        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        //en este podemos poner un path mas personalizado, cuando lo pones la ruta sera/api/nombrecontrolador/login
        [HttpPost("login")]
        //en este caso mandaremos en el body de la solicitud un objeto model
        public IActionResult Identify([FromBody] AuthRequest model)
        {
            Response res = new Response();
            
            UserResponse userResponse = _userService.Auth(model);

            if (userResponse == null)
            {
                res.Success = 0;
                res.Message = "Usuario o Contraseña incorrecta";
                //este metodo esta dado por si se tiene algun error en la solicitud , ya lo tienen definido
                return BadRequest(res);
            }
            res.Success = 1;
            res.Data = userResponse;

            return Ok(res);
        }
    }
}
