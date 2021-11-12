using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Tools;

namespace WSVenta.Services
{
    public class UserService : IUserService
    {
        //este metodo nos lo indica la interfaz
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse res = new UserResponse();
            using (var bd = new VENTA_REALContext())
            {
                
                string sPassword = Encrypt.GetSHA256(model.Password);
                //este te trae el usuario que tenga ambos campos iguales 
                var usuario = bd.User.Where(d => d.Email == model.Email && d.Password == sPassword).FirstOrDefault();

                if (usuario == null) return null;

                res.Email = usuario.Email;
            }
            return res;

        }

    }
}
