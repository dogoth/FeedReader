using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FeedReader.Model;
using Microsoft.EntityFrameworkCore;

namespace FeedReader
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "Server=127.0.0.1\\SQL2016;Database=NewsCrud_DB;User Id=app_newscrud;Password=app_newscrud;";
            services.AddDbContext<NewsCrud_DBContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<Interfaces.IQueueContext, Queue.AzureQueue>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();




            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("FeedReader Server Started");
            //});
        }
    }
}
