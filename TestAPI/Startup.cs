using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestAPI.Models;


namespace TestAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //Define Connection String.
            var connection = @"Server=(localdb)\mssqllocaldb;Database=master;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<masterContext>(options => options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
        
    }
}
