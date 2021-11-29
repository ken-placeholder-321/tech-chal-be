using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.WebAPI.Db;
using TestProject.WebAPI.Logger;
using TestProject.WebAPI.Repository;
using TestProject.WebAPI.Services;

namespace TestProject.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<ISimpleLogger, SimpleLogger>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAccountService, AccountService>();

            services.AddDbContext<ZipProjectDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
  
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
