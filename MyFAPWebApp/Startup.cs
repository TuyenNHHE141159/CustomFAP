using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFAPWebApp.Models;

namespace MyFAPWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<MyFapContext>(o =>
            //o.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyDB")));
            //services.AddScoped(typeof(MyFapContext));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=StudentHome}/{action=Index}?{student_id?&&subject_id?}"
                    );
            });
        }
    }
}
