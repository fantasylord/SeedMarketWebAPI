
using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Entity;
using CoreBackend.Api.Repositories;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services;
using CoreBackend.Api.Services.Iservices;
using CoreBackend.Api.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace CoreBackend.Api
{
    public class Startup
    {
        /// <summary>
        ///配置了一个自定义服务类
        /// </summary>
        public static IConfiguration Configuration { get; private set; }//读取配置文件

        public MapperConfiguration MapperConfiguration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
           
            //服务器资源路径置换，这样可以防止客户端猜测服务端文件路径，制造一个虚拟的隐射进行访问，提高了安全性。
          
            var builder=new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        /// <summary>
        /// ConfigureServices方法是用来把services(各种服务, 
        /// 例如identity, ef, mvc等等包括第三方的, 
        /// 或者自己写的)加入(register)到container(asp.net core的容器)中去,
        /// 并配置这些services. 
        /// 这个container是用来进行dependency injection的(依赖注入). 
        /// 所有注入的services(此外还包括一些框架已经注册好的services) 
        /// 在以后写代码的时候, 都可以将它们注入(inject)进去. 
        /// 例如上面的Configure方法的参数, app, env, loggerFactory都是注入进去的services.
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//注册MVC服务
            
            services.AddMvcCore()
                .AddAuthorization()//注册验证服务器到DI
                .AddJsonFormatters()
                ;
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = "http://localhost:5005";//指定auth服务器
                    options.ApiName = "socialnetwork";//与authorization server中的apiresource对应;
                }
                );
      


            //跨域
            //string[] urls = Configuration.GetSection("AllowCors:AllowAllOrign").Value.Split(',');
            //services.AddCors(o =>
            //{
            //    o.AddPolicy("AllowAllOrign", builder => {
            //        builder.WithOrigins(urls)
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials();
            //    });
            //}
            //);//跨域 注还需要在configure配置或者controller[EnableCors("AllowAllOrign")]




            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                );

            //添加IIS kestrel支持
            services.Configure<IISOptions>(o=>o.ForwardClientCertificate=false);

             
            //使用AddDbContext这个Extension method为MyContext在Container中进行注册，它默认的生命周期使Scoped。

            services.AddMvc().AddJsonOptions(o =>
            {
                if (o.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                {
                    resolver.NamingStrategy = null;
                }
            });//将json格式自动首字母大写

            //services.AddMvc()
            //    .AddMvcOptions(p =>
            //{
            //    p.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //    p.OutputFormatters.Add(new XmlSerializerOutputFormatter());

            //});//添加对xml格式的格式化输出

            services.AddSwaggerGen(c =>

            {

                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "测试服务器",
                        Version = "v1",
                        Description = "一个商城用例服务器\n " +
                        "如若需要用到Enum(枚举类型)的值请参照枚举类接口查询值\n" +
                        "服务处于测试阶段拒绝商业用途",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Fantasy_Lord", Email = "1819751663@qq.com", Url = "" },
                        License = new License { Name = "小白无理由拒绝", Url = "" }
                    });
           
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
         "CoreBackend.Api.xml")); // 注意：此处替换成所生成的XML documentation的文件名。
                c.DescribeAllEnumsAsStrings();
               
                c.OperationFilter<SwaggerFileUploadFilter>();


            });//配置swagger
               /// ConfigureServices方法。这里面有三种方法可以注册service
               ///AddTransient，AddScoped和AddSingleton，
               ///这些都表示service的生命周期.
               ///transient的services是每次请求（不是指Http request）都会创建一个新的实例，
               ///它比较适合轻量级的无状态的（Stateless）的service。
               ///scope的services是每次http请求会创建一个实例
               ///singleton的在第一次请求的时候就会创建一个实例，
               ///以后也只有这一个实例，或者在ConfigureServices这段代码运行的时候创建唯一一个实例。
               services.AddTransient<IFileService,FileService>();
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
                        services.AddTransient<IMailService, CloudMailService>();
#endif
            services.AddScoped<ISeedRepository, SeedRepository>();
            try
            {
               // string connectionstr = Configuration["mysqlSettings:ConnectionString"];
                string connectionstr1 = Configuration["mysqlSettings:ConnectionLocalString"];

                //services.AddDbContext<MyContext>(o =>
                //{
                //    o.UseMySQL(connectionstr);

                //});

                services.AddDbContext<ProductContext>(o =>
                {
                    o.UseMySQL(connectionstr1);
                  
                });
            }
            catch (Exception e)
            {
                string ss = e.ToString();
                throw;
            }

            //需要IMailService的一个实现的时候，Container就会提供一个LocalMailService的实例。
            services.AddMvcCore().AddApiExplorer();



            //Startup.Configuration["mailSettings:mailFromAddress"];

            //   var connectionString = @"Server=(localdb)\ProjectsV13;Database=ProductDB;Trusted_Connection=True";
            //   string s = $"测试数据库连接字符串{Configuration["sqlSettings:ConnectionString"]}";

            //   services.AddDbContext<MyContext>(o => o.UseSqlServer(connectionString)).BuildServiceProvider();



        }



        /// <summary>
        /// Configure方法是asp.net core程序用来具体指定如何处理每个http请求的,
        /// 例如我们可以让这个程序知道我使用mvc来处理http请求, 
        /// 那就调用app.UseMvc()这个方法就行. 但是目前, 所有的http请求都会导致返回"Hello World!"
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="productContext"></param>
        /// <param name="myContext"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory,
                                 ProductContext productContext)

        {

            loggerFactory.AddNLog();
            loggerFactory.AddDebug();//填写日志

            if (env.IsDevelopment())

            {

                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler();
            }

            //使用日志服务
            // loggerFactory.AddProvider(new NLogLoggerProvider());

            //跨域使用
            app.UseCors(builder => {
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowAnyOrigin();
            });
      //      app.UseCors("AllowAllOrign");
       


            //status code middleware 提供显示错误信息时的友好界面显示格式
            app.UseStatusCodePages();

            productContext.EnsureSeedDataForContext();
            //  myContext.EnsureSeedDataForContext();   //数据访问初始化


            //sweagger服务添加
            app.UseMvcWithDefaultRoute();


            //注册AutoMapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductWithoutMaterialDto>();
                cfg.CreateMap<Product, ProductDot>();
                cfg.CreateMap<Material, MaterialDto>();
                cfg.CreateMap<Product, ProductCreation>();
                cfg.CreateMap<ProductCreation, ProductCreation>();
                cfg.CreateMap<ProductCreation, Product>();
                cfg.CreateMap<ProductModification, Product>();
                cfg.CreateMap<Product, ProductModification>();
                cfg.CreateMap<BuyerDto, Buyer>();
                cfg.CreateMap<Buyer, BuyerDto>();
                cfg.CreateMap<Buyer, BuyerCreation>(); 
                cfg.CreateMap<BuyerCreation, Buyer>();
                cfg.CreateMap<Indent, IndentDto>();
                cfg.CreateMap<IndentDto, Indent>();
                cfg.CreateMap<Indent, Indent>();

            });

            app.UseSwagger(c =>

            {


            });

            app.UseSwaggerUI(c =>

            {

                c.ShowExtensions();

                c.ValidatorUrl(null);
                
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "测试服务器 V1");


            });
            app.UseAuthentication();//添加验证中间件
         
            app.UseMvc();
        }
    }
}
