using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  IdentityServer.Services;

namespace IdentityServer.Services.impl
{
    /// <summary>
    /// 手机号码验证服务
    /// </summary>
    public class TestAuthCodeService : IAuthCodeService
    {
        /// <summary>
        ///  验证手机号码是否正确
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="auth_code">短信验证码</param>
        /// <returns></returns>
        public bool Validate(string phone, string auth_code)
        {
            return true;
        }
    }
}
