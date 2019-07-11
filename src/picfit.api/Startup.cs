using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using picfit.application.Commands;
using picfit.application.Infrastructure;
using picfit.application.Infrastructure.Image;
using picfit.application.Infrastructure.Storage;
using picfit.infrastructure.Image;
using picfit.infrastructure.Image.ImageSharp;
using picfit.infrastructure.Storage;
using Swashbuckle.AspNetCore.Swagger;

namespace picfit.api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCustomSwagger();
            services.AddCustomMediatR();
            services.AddCustomStorageService(Configuration);
            services.AddCustomImagePreProcessingService(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
    }
    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomImagePreProcessingService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImagePreProcessingConfig>(
                configuration.GetSection("imagepreprocessing"));
            services.AddScoped<IImageProcessingFactory, ImagePreProcessingFactory>();
            return services;
        }
        public static IServiceCollection AddCustomStorageService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StorageConfig>(
                configuration.GetSection("storage"));
            services.AddScoped<IStorageFactory, StorageFactory>();
            return services;
        }

        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestCacheBehavior<,>));
            services.AddMediatR(typeof(AddImageCommandHandler).GetTypeInfo().Assembly);
            return services;
        }
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "picfit webapi", Version = "v1" });

                //c.DocumentFilter<EnumDocumentFilter>();

                string paymentsApiXmlFilePath = Path.Combine(AppContext.BaseDirectory, "picfit.api.xml");
                c.IncludeXmlComments(paymentsApiXmlFilePath);

                //c.AddFluentValidationRules();
            });
            return services;
        }
    }
}
