using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region 为网关基础identity服务授权验证.文档地址:https://ocelot.readthedocs.io/en/latest/features/authentication.html#identity-server-bearer-tokens

            var authenticationScheme = "secret";//就是我们identityConfig中定义的secret

            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationScheme, o =>
                {
                    o.Authority = "http://localhost:55219/";//需要填写我们授权中心的地址也就是identityServer的地址
                    o.ApiName = "Gateway";//这里我们给网关授权。在微服务里面只需要统一给网关授权就行。网关承载着整个系统的入口
                    o.SupportedTokens = SupportedTokens.Both;
                    o.ApiSecret = "secret";//就是我们identityConfig中定义的secret
                    o.RequireHttpsMetadata = false;//是否必须使用https
                });

            #endregion


            //添加网关默认配置
            // 相关文档地址:https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication()//注册需要权限验证
                .UseOcelot().Wait();
        }
    }
}
