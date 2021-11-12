using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVenta.Models.Response
{
    public class UserResponse
    {
        public string Email { get; set; }
        //el token nos sirve para poder tener digamos una credencial,y no pedir identificacion en un determinado tiempo
       //una vez realizado, mostraras el token , jwt
        public string Token { get; set; }
    }
}
