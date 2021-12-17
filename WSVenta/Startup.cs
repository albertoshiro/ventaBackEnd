using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSVenta.Models.Common;
using WSVenta.Services;

namespace WSVenta
{
    public class Startup
    {
        readonly string MiCors = "MiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy(name: MiCors,
                    builder =>
                    {
                        //que hacepte todos los tipos de dato para post(insercion de datos)
                        builder.WithHeaders("*");
                        //para traer datos de la bd
                        builder.WithOrigins("*");
                        //esta es para los metodos put y delete
                        builder.WithMethods("*");
                    }

                    );
            });
            //configurar jwt
            var appSettingsSection = Configuration.GetSection("AppSettings");


            //agregando la cadena secreto a los servicios mediante injeccion de la cadena de texto dada en una clase 
            services.Configure<AppSettings>(appSettingsSection);



            //jwt
            var appSettings = appSettingsSection.Get<AppSettings>();
            //esta de aca tiene el secreto, como tal se saca el apartado secret de appSetings
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
            services.AddAuthentication(d =>
            {
                //aqui necesitamos agragar paquetes nugget para esto
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(d=>
            {
                d.RequireHttpsMetadata = false;
                //para qeue tenga una vida el token
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //este es la clave , le pasamos nuestra secreot(palabra clave) en esta instrucción
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
        );
            //injectando, en este caso scoped, que el objeto exista por cada solicitud

            //Esto ya esta inyectado, por lo que podriamos utilizarlo en cada clase de este programa
            services.AddScoped<IUserService, UserService>();
            //aca injectamos el servicio paracumplir con lo de la "D" del principio solid
            services.AddScoped<ISaleService,SaleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //aqui agregas el cors que creaste arriba como variab.e de solo lectura

            app.UseCors(MiCors);

            //decimos que  usaremos autentificacion necesario para el jwt
            app.UseAuthentication();

            app.UseAuthorization();

          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
