using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Domain;
using Movie.Service.Movie;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Infrastructure.HostedService;
using MovieWebAPI.Infrastructure.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace MovieWebAPI
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
            #region DB
            services.AddDbContext<MovieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region AutoMapper
            services.AddAutoMapper();
            AutoMapperConfiguration.Init();
            #endregion

            #region Service DI
            services.AddScoped<IMovieRepository, MovieService>();
            #endregion

            #region AppSettings
            services.Configure<DataSource>(Configuration.GetSection("DataSource"));
            services.Configure<Caching>(Configuration.GetSection("Caching"));
            #endregion

            //#region HostedService
            //services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, DataSourceService>();
            //services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, RequestCollectorService>();
            //#endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Movie",
                    Description = "Movie API",
                    TermsOfService = "Movie Swagger"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            #region MemCache
            services.AddMemoryCache();
            #endregion

            #region Mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            #endregion
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
                app.UseHsts();
            }

            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
                c.DocExpansion(DocExpansion.List);
            });
            #endregion

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
