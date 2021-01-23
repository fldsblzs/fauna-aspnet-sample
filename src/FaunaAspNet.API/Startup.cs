using AutoMapper;
using FaunaAspNet.API.Configuration;
using FaunaAspNet.API.Messages;
using FaunaAspNet.API.Repositories;
using FaunaAspNet.API.Validation;
using FaunaDB.Client;
using FaunaDB.Types;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FaunaAspNet.API
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
            services
                .AddControllers()
                .AddFluentValidation();
            
            services.AddTransient<IValidator<ArtistMessage>, ArtistMessageValidator>();
            services.AddAutoMapper(typeof(Startup));
            services.Configure<FaunaOptions>(Configuration.GetSection(FaunaOptions.Fauna));
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddSingleton(sp =>
            {
                var faunaSettings = sp.GetRequiredService<IOptionsMonitor<FaunaOptions>>().CurrentValue;
                return new FaunaClient(faunaSettings.Secret, faunaSettings.Endpoint);
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FaunaAspNet.API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaunaAspNet.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}