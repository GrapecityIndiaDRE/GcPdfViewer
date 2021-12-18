using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportApi.Controllers;

namespace GcPdfViewerSupportApiDemo
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
            services.AddRazorPages();
            services.AddMvc((opts) => { opts.EnableEndpointRouting = false; });
            services.AddRouting();
            services.AddCors(cors => cors.AddPolicy(name: "All", policy =>
              {
                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
              }));
            SupportApi.Connection.GcPdfViewerHub.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SupportApi.Connection.GcPdfViewerHub.Configure(app);
            GcPdfViewerController.Settings.VerifyToken += VerifyAuthToken;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("All");

            app.UseAuthorization();


            app.UseMvcWithDefaultRoute();
        }

        private void VerifyAuthToken(object sender, SupportApi.Models.VerifyTokenEventArgs e)
        {
            string token = e.Token;
            if (string.IsNullOrEmpty(token) || !token.Equals("support-api-demo-net-core-token-2021"))
            {
                e.Reject = true;
            }
        }
    }
}
