using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using diagnoseApp.DAL;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace diagnoseApp
{
    [ExcludeFromCodeCoverage]
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
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        // må være med når det skal serialiseres "kompliserte" strukturer til JSON. 
        // i tillegg må Microsoft.AspNetCore.NewtonsoftJson installeres som pakke
        );
            services.AddDbContext<PersonDB>(options => options.UseSqlite("Data Source=Person.db"));

            services.AddScoped<IPersonRepository, PersonRepository>();

            // I tillegg til under må pakken Microsoft.AspNetCore.Session legges til i NuGet
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(1800); // 30 minutter
                options.Cookie.IsEssential = true;
            });
            
            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddFile("Logs/PersonLog.txt");
                DBInit.init(app);// denne må fjernes dersom vi vil beholde dataene i databasen og ikke initialisere 
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // UseSession!
            app.UseSession();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

