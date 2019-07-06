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
            #endregion

            //#region HostedService
            //services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, DataSourceService>();
            //services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, RequestCollectorService>();
            //#endregion

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

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
