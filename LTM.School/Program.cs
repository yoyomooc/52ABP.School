using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LTM.School.EntityFramework;
using LTM.School.EntityFramework.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LTM.School
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope=host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SchoolDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception e)
                {
                    //初始化系统测试数据的时候报错，请联系管理员。
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "初始化系统测试数据的时候报错，请联系管理员。");

                }





            }



            host.Run();
            


             
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();
    }
}
