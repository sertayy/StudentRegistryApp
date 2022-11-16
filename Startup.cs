using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentRegistryApp.Data;
using StudentRegistryApp.Services;
using System;

[assembly: WebJobsStartup(typeof(StudentRegistryApp.Startup))]
namespace StudentRegistryApp
{
    public class Startup: IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            //var sqlConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString") ?? string.Empty;
            //builder.Services.AddDbContext<StudentContext>((options) =>
            //    options.UseSqlServer(sqlConnectionString, sqlOptions =>
            //        sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(2), null)
            //    )
            //);
            builder.Services.AddDbContext<StudentContext>();
            builder.Services.AddScoped<IStudentService, StudentService>();
        }
    }
}
