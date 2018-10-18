using System;
using System.IO;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Features.Workouts.AddWorkout.v1;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api
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
            var appSettings = BuildAppSettings();
            
            services.AddDbContext<SebastianDbContext>(opt => 
                opt.UseSqlServer(appSettings.Database.ConnectionString));
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddWorkoutCommandValidator>());

            services.AddMediatR();

            services.AddSingleton(appSettings);
            services.AddScoped<IUserPrincipal>(context =>
            {
                var db = context.GetService<SebastianDbContext>();
                var user = db.Find<User>(Guid.Parse("59DC3D2F-DC14-41F2-A75E-1371FC866AE8"));
                return new UserPrincipal(user);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static AppSettings BuildAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var appConfig = new AppSettings();
            config.Bind(appConfig);
            return appConfig;
        }
    }
}