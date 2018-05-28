using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndroidPitalica.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AndroidPitalica
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
            services.AddMvc();

            services.AddDbContext<PitalicaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EntitiesConnection")));

            services.AddMvc()
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "TryGetUser",
                    template: "Users/TryGetUser",
                    defaults: new { controller = "GuestMeals", action = "TryGetUser" });

                routes.MapRoute(
                name: "ExamsTaken",
                template: "Exams/GetExamsTaken/{id?}",
                defaults: new { controller = "Exams", action = "GetExamsTaken" },
                    constraints: new { id = new IntRouteConstraint() }
                    );

                routes.MapRoute(
               name: "InsertExam",
               template: "Exams/InsertExam",
               defaults: new { controller = "Exams", action = "InsertExam" }
                   );

                routes.MapRoute(
                name: "ExamsNotTaken",
                template: "Exams/GetExamsNotTaken/{id?}",
                defaults: new { controller = "Exams", action = "GetExamsNotTaken" },
                    constraints: new { id = new IntRouteConstraint() }
                    );

                routes.MapRoute(
                name: "ExamsCreated",
                template: "Exams/GetExamsCreated/{id?}",
                defaults: new { controller = "Exams", action = "GetExamsCreated" },
                    constraints: new { id = new IntRouteConstraint() }
                    );

                routes.MapRoute(
                name: "ExamQuestions",
                template: "Questions/GetExamQuestions/{id?}",
                defaults: new { controller = "Questions", action = "GetExamQuestions" },
                    constraints: new { id = new IntRouteConstraint() }
                    );

                routes.MapRoute(
                name: "ExamResults",
                template: "Exams/GetExamResults/{examId?}/{userId?}",
                defaults: new { controller = "Exams", action = "GetExamResults" },
                    constraints: new { examId = new IntRouteConstraint(), userId = new IntRouteConstraint() }
                    );
            });
        }
    }
}
