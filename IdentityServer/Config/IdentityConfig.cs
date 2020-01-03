using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Config
{
    /// <summary>
    /// 配置相关文档:http://docs.identityserver.io/en/latest/reference/client.html#client
    ///              http://docs.identityserver.io/en/latest/topics/resources.html
    /// </summary>
    public class IdentityConfig
    {
       
        public static IEnumerable<IdentityResource> GeIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address()
            };
        }

        public static IEnumerable<ApiResource> GeApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("Gateway","User Server") //支持可访问的resource
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {

                    ClientId="Android",
                    RefreshTokenExpiration=TokenExpiration.Sliding,//设置令牌过期类型为:相对过期
                    RequireClientSecret = false,//是否需要Secret来重新刷新token
                    AllowOfflineAccess = true,//指定此客户端是否可以请求刷新token
                    AlwaysIncludeUserClaimsInIdToken=true,//当同时请求ID令牌和访问令牌时，应该将用户声明始终添加到ID令牌中，而不是要求客户端使用userinfo端点。默认值为false。
                    AllowedGrantTypes =new List<string>(){"sms_auth_code"},//这里是我们自定义验证里面定义的GrantType
                    ClientSecrets=new List<Secret>()
                    {
                        new Secret("secret".Sha256())//设置客户端凭证模式秘钥
                    },
                    AllowedScopes = new List<string>()
                    {
                        "Gateway",
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    } //允许访问的resource
                }
            };
        }
    }
}
