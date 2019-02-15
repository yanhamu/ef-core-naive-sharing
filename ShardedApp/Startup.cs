using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShardedApp.DataAccess;

namespace ShardedApp
{
    public class Startup
    {
        private readonly IConfigurationRoot config;

        public Startup()
        {
            this.config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddScoped(x => CreateTenant(x.GetService<IHttpContextAccessor>(), config.GetConnectionString("GenericConnection")));
            services.AddDbContext<ShardingContext>();
        }

        private AppTenant CreateTenant(IHttpContextAccessor contextAccessor, string baseConnectionString)
        {
            var naiveKey = int.Parse(contextAccessor.HttpContext.Request.Headers["naive-key"]);
            return new AppTenant(MappingFunction(baseConnectionString, naiveKey));
        }

        private string MappingFunction(string baseConnectionString, int customerId)
        {
            return string.Format(baseConnectionString, customerId % 2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}