using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestAPI.Models;


namespace TestAPI
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddDbContext<ContractContext>(opt => opt.UseInMemoryDatabase("ContractList"));
            services.AddMvc();
            var connection = @"Server=(localdb)\mssqllocaldb;Database=master;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<masterContext>(options => options.UseSqlServer(connection));
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    //ToDo: Implement Logger Factory
        //    loggerFactory.AddConsole(System.Configuration.GetSection("Logging"));
        //    loggerFactory.AddDebug();
        //    loggerFactory.AddAzureWebAppDiagnostics();
        //    //loggerFactory.AddDebug();

        //    /* nlog */
        //    loggerFactory.AddNLog();
        //    env.ConfigureNLog("NLog.config");

        //    /*
        //     * Shows UseCors with CorsPolicyBuilder.
        //     * global policy - assign here or on each controller
        //     */
        //    app.UseCors("CorsPolicy");

        //    app.UseMvc();

        //    /* Enable middleware to serve generated Swagger as a JSON endpoint. */
        //    app.UseSwagger();

        //    /* Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint. */
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //        c.ConfigureOAuth2(AuthenticationDto.Instance.Swagger.ClientId, AuthenticationDto.Instance.Swagger.ClientSecret, AuthenticationDto.Instance.Swagger.Realm, AuthenticationDto.Instance.Swagger.AppName, "", new Dictionary<string, string>() { { "resource", AuthenticationDto.Instance.Swagger.Resource } });
        //    });
        //}
        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseMvc();
        //}
    }
}
