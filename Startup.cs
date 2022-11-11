using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentRegistryApp.Data;
using StudentRegistryApp.Services;

namespace StudentRegistryApp
{
    public class Startup
    {
        // Use this method to add services to the container.  
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StudentContext>(options => options.UseSqlServer(configuration.GetConnectionString("StudentRegistryAppConnectionString")));
            services.AddScoped<IStudentService, StudentService>();
            services.AddRazorPages();
            //services.AddHttpContextAccessor();
            //services.AddControllers();
            //services.AddTransient<IStudentService, StudentService>();
            //services.AddMvc();

        }
        // Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
