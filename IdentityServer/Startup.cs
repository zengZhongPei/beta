using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer.Authentication;
using IdentityServer.Config;
using IdentityServer.Services;
using IdentityServer.Services.impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer
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
            //配置完成后调用获取token的Url来进行授权 /connect/token
            //   相关示例文档:http://docs.identityserver.io/en/latest/endpoints/authorize.html

            #region 新增identity的配置

            /*共4个步骤:  参考文献:http://docs.identityserver.io/en/aspnetcore2/quickstarts/1_client_credentials.html
               1.添加Nuget包:identityServer4
               2.添加Startup.cs配置类
               3.添加identityServerConfig配置类
               4.更改IdentityServer4配置。把identityServerConfig添加进去
                   4.1 可以使用identityServer的扩展验证方法来实现自定义验证支持添加多个自定义验证。获取token的时候使用参数 grant_type来区分
               5.添加客户端配置
            */

            services.AddIdentityServer()
                .AddExtensionGrantValidator<SmsAuthCodeGrantType>()//把自定义的验证添加进去
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityConfig.GeApiResources())//添加配置文件后把这个添加到配置里面去
                .AddInMemoryIdentityResources(IdentityConfig.GeIdentityResources())
                .AddInMemoryClients(IdentityConfig.GetClients())//添加配置文件后把这个添加到配置里面去
                ;

            
            #endregion

            services.AddScoped<IAuthCodeService,TestAuthCodeService >()
                .AddScoped<IUserService,UserService>();

            //注册全局单例请求
            services.AddSingleton(new HttpClient());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
