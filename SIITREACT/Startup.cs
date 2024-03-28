using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIITREACT.Model;
using SIITREACT.Service;
using System;

namespace SIITREACT
{
    public class Startup
    {
        private IServiceProvider serviceProvider;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            var connectionString = "Server=127.0.0.1;Database=world;Uid=root;Pwd=sqlroot;";
            services.AddDbContext<MyDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

          services.AddIdentity<ApplicationUser, IdentityRole>()
         .AddEntityFrameworkStores<MyDbContext>()
      .AddDefaultTokenProviders();

            services.AddControllers();

            services.AddScoped<AppointmentService>();
          
            services.AddScoped<UserService>();
          


            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddControllers();
        }

     

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseCors("AllowLocalhost3000");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
       
    }
}


