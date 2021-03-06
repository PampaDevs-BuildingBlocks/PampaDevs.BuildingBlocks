using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PampaDevs.BuildingBlocks.Bus;
using PampaDevs.Bus.InProc;
using PampaDevs.Bus.InProc.Notifications;
using WebStore.ProductCatalog.Domain.Usecases.CreateProduct;

namespace WebStore.ProductCatalog.Api
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
            services.AddInProcBus(typeof(Startup), typeof(CreateProductCommand));
            services.AddScoped<IRequestHandler<CreateProductCommand, CreateProductCommandResponse>, CreateProductHandler>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebStore.ProductCatalog.Api",
                    Description = "Sample project for using PampaDevs.BuildingBlocks",
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe de Almeida",
                        Email = "felipe.allmeida.dev@gmail.com",
                        Url = new Uri("https://felipe-allmeida.github.io/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT"
                    }                    
                });
                

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.ProductCatalog.Api V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
