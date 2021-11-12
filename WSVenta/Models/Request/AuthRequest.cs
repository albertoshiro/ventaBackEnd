using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WSVenta.Models.Request
{
    public class AuthRequest
        //esta clase la pediremos o resiviremos al autoidentificarnos
        //cuando se quiere loguear necesita los 2 si uno falta no se puede 
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
