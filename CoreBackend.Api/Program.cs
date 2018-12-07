using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace CoreBackend.Api
{
    public class Program
    {

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            
            if (!Directory.Exists(ServiceConfigs.FileUpDirectory))
            {
                Directory.CreateDirectory(ServiceConfigs.FileUpDirectory);
            }
            //   CreateDefaultBuilder(args).Build();
            //CreateDefaultBuilder(args).Build();


        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseUrls("http://*:5000")
               .UseKestrel()//kestrel 解决默认绑定IP地址与端口问题
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .UseApplicationInsights()
               .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>

    }
}
