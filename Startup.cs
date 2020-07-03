using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDCore3Ang7.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CRUDCore3Ang7
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
         
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MinhaConexao")));

            //Abre pra tudo que for método e forma de acessar a API seja possível (CORS: 'ompartilhamento de recursos de origem cruzada')
            //A melhor forma de fazer isto seria delimitando cada um dos métodos de API com uma regra!
            //<!--https://docs.microsoft.com/pt-br/aspnet/core/security/cors?view=aspnetcore-3.1#attr-->
            //Segundo o Macoratti: Temos que habilitar o CORS no projeto WEB API para poder enviar uma requisição a partir da aplicação Angular que vai estar em outro domínio.
            //Não sei se isso não libera muito a segurança e se quando haja alguma outra forma quando um WEB.API tiver uma segurança por tokens isto possa ser o bastante para não ter problema de segurança.
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Chama o serviço criado para liberar tudo da CORS
            app.UseCors("EnableCORS");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
