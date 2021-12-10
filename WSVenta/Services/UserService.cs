using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Common;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Tools;

namespace WSVenta.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
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
                res.Token = GetToken(usuario);
            }
            return res;
        }

        private string GetToken(User usuario )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            //aqui se resivira un arreglo de claims
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            //aca ponemos lo que se guardara en el token
                            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Email, usuario.Email)
                        }
                    ),
                //cuanto toempo vivirá el token
                Expires = DateTime.UtcNow.AddDays(60),
                //la sguente instruccion es la que encripta el cofigo , se l e pasa la llave y el algoritmo por el cual encriptaremos
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
