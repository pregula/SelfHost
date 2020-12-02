using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SelfHost.DataAccess;
using SelfHost.Domain.Repositories;
using SelfHost.Framework;
using SelfHost.Infrastructure.Abstractions.Interfaces;
using SelfHost.Infrastructure.Mappers;
using SelfHost.Infrastructure.Repositories;
using SelfHost.Infrastructure.Services;

namespace SelfHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SelfHostDB;Integrated Security=True; Trusted_Connection=True";
            services.AddDbContext<ApplicationDbContext>
                (options =>
                {
                    options.UseSqlServer(connectionString);
                });

            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}