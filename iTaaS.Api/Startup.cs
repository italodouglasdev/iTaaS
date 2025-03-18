using iTaaS.Api.Aplicacao.Interfaces.Mapeadores;
using iTaaS.Api.Aplicacao.Interfaces.Repositorios;
using iTaaS.Api.Aplicacao.Interfaces.Servicos;
using iTaaS.Api.Aplicacao.Mapeadores;
using iTaaS.Api.Aplicacao.Servicos;
using iTaaS.Api.Infraestrutura.BancoDeDados;
using iTaaS.Api.Infraestrutura.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace iTaaS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextoServico, HttpContextoServico>();
           
            services.AddScoped<ILogServico, LogServico>();
            services.AddScoped<ILogRepositorio, LogRepositorio>();
            services.AddScoped<ILogMapeador, LogMapeador>();
            services.AddScoped<ILogLinhaServico, LogLinhaServico>();
            services.AddScoped<ILogLinhaRepositorio, LogLinhaRepositorio>();
            services.AddScoped<ILogLinhaMapeador, LogLinhaMapeador>();            

            services.AddTransient<ILogTipoFormatoServico, LogTipoFormatoMinhaCdnServico>();
            services.AddTransient<ILogTipoFormatoServico, LogTipoFormatoAgoraServico>();

                        
            services.AddDbContext<EntityContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ItaaS - Conversor de Logs API",
                    Version = "v1",
                    Description = "API de importação, transformação e gerenciamento de logs, oferecendo endpoints para criação, busca e manipulação de logs em diversos formatos.",
                    Contact = new OpenApiContact
                    {
                        Name = "Italo Douglas Vasconcelos de Alencar",
                        Email = "italodouglas.dev@gmail.com",
                        Url = new Uri("https://italodouglas.dev")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.DescribeAllParametersInCamelCase();

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
     
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "iTaaS API v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
