using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Data;

namespace OdeToFood
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
            // entity provides methods that can be used to discribe the db context that this app is going to use
            // adddbcontextpool pools dbcontexts in an attempt to reuse dbcontexts that have been created while 
            // the app is alive which can lead to better performance and scalablility

            services.AddDbContextPool<OdeToFoodDBContext>(options => {
                // the param is which sql server you want to use.
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            // for in memory data storage config
            // services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            // for database storage config
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // middleware


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // Tells the browser to only load over a secure connection
                app.UseHsts();
            }

            app.Use(SayHelloMiddleware);


            // sends redirct to any browser using plain http
            app.UseHttpsRedirection();
            // serves static files from wwwroot folder
            app.UseStaticFiles();
            // serve static files from node modules folder
            app.UseNodeModules(env);
            app.UseCookiePolicy();

            // router
            app.UseMvc();
        }


        // request delegate is a method that takes an http context as a paramater and returns a task
        private RequestDelegate SayHelloMiddleware(RequestDelegate next)
        {
            return async ctx =>
            {

                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                     await ctx.Response.WriteAsync("Hello World");
                }
                else
                {
                    await next(ctx);
                }
            };
        }
    }
}
