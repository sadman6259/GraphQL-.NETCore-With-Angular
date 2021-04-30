using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL.API.GraphqlCore;
using GraphQL.API.Infrastructure.DBContext;
using GraphQL.API.Infrastructure.Repositories;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GraphQL.API
{
    public class Startup
    {
        readonly string AllowMyOrigin = "_AllowMyOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TechEventInfoType>();
            services.AddSingleton<ParticipantType>();
            services.AddSingleton<EmployeeAttendenceType>();

            services.AddSingleton<TechEventQuery>();
            services.AddTransient<ITechEventRepository, TechEventRepository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddDbContext<TechEventDBContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("GraphQLDBConnection")));
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new TechEventSchema(new FuncDependencyResolver(type => sp.GetService(type))));
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowMyOrigin",
            //    builder => builder.WithOrigins("http://localhost:4200"));
            //});
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AllowMyOrigin");
           // app.UseHttpsRedirection();

            app.UseGraphiQl("/graphql");
            app.UseMvc();
        }
    }
}
